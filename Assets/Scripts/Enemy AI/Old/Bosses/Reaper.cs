using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject player;
    private Player playerScript;

    [SerializeField]
    private Enemy enemy;

    private LayerMask PlayerMask;
    private LayerMask PlayerHitDetectMask;
    private LayerMask SpawnerMask;

    public float aggroRange = 15f;
    public float Speed = 4f;
    public int Damage = 15;
    public float AttackInterval = 1f;
    public float hitRange = 3f;

    public float damageIntervalReset = 0.5f;
    private float damageInterval = 0;

    private bool facingRight = true;
    private bool canInflictDamage = true;

    private Transform playerPos;

    private float enemyRot;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();
        PlayerMask = LayerMask.GetMask("Player");
        PlayerHitDetectMask = LayerMask.GetMask("PlayerHitDetector");

        SpawnerMask = LayerMask.GetMask("Spawner");

        rb = GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
    }

    private void Start() {
        StartCoroutine(AttackCoroutine(AttackInterval));
    }

    private void FixedUpdate() {

        if (CheckForPlayer()) {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, Speed * Time.deltaTime);

            if (facingRight == false && playerPos.position.x > transform.position.x) {
                Flip();
            }else if (facingRight == true && playerPos.position.x < transform.position.x) {
                Flip();
            }
        } 
        // else if (!CheckForSpawner()) {
        //     enemy.Relocate();
        // }

    }

    private IEnumerator AttackCoroutine(float interval) {
        yield return new WaitForSeconds(interval);

        if (CheckIfPlayerCanBeHit()) {
            enemy.anim.SetTrigger("Attack");
        }

        StartCoroutine(AttackCoroutine(AttackInterval));
    }

    void OnCollisionStay2D(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();

        damageInterval -= Time.deltaTime;

        if (damageInterval <= 0) {
            canInflictDamage = true;
        } else {
            canInflictDamage = false;
        }

        if (player != null && canInflictDamage) {
            // player.TakeDamage(Damage);
            damageInterval = damageIntervalReset;
        }

    }

    void OnCollisionExit2D(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();

        

        if (player != null) {
            damageInterval = 0;
            canInflictDamage = true;
        }

    }

    public bool CheckForPlayer() {
        return Physics2D.OverlapCircle(transform.position, aggroRange, PlayerMask);
    }

    public bool CheckIfPlayerCanBeHit() {
        return Physics2D.OverlapCircle(transform.position, hitRange, PlayerHitDetectMask);
    }

    public bool CheckForSpawner() {
        return Physics2D.OverlapCircle(transform.position, 0.1f, SpawnerMask);
    }

    void Flip() {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    public void DamagePlayer() {
        if (CheckIfPlayerCanBeHit()) {
            playerScript.anim.SetTrigger("Hit");
            // playerScript.TakeDamage(Damage);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
