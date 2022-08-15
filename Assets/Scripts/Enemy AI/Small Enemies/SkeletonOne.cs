using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonOne : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject player;
    private Player playerScript;

    [SerializeField]
    private Enemy enemy;

    private LayerMask PlayerMask;
    private LayerMask SpawnerMask;

    public float aggroRange = 15f;
    public float Speed = 4f;
    public int Damage = 15;
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

        rb = GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
    }

    private void FixedUpdate() {

        if (CheckForPlayer()) {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, Speed * Time.deltaTime);

            if (facingRight == false && playerPos.position.x > transform.position.x) {
                Flip();
            }else if (facingRight == true && playerPos.position.x < transform.position.x) {
                Flip();
            }
        } else if (!CheckForSpawner()) {
            enemy.Relocate();
        }

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
            player.TakeDamage(Damage);
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

    public bool CheckForSpawner() {
        return Physics2D.OverlapCircle(transform.position, 0.1f, SpawnerMask);
    }

    void Flip() {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
