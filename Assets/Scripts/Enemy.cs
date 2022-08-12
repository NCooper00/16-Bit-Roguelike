using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject player;
    private Player playerScript;

    [SerializeField]
    private GameObject DamageUp;

    private EnemySpawner SPAWNER;

    // private Player PLAYER;

    public float aggroRange = 15f;
    private LayerMask PlayerMask;
    private LayerMask SpawnerMask;

    public int Health = 100;
    public int Damage = 15;
    public float Speed = 4f;

    public float offset = 0f;
    public float damageIntervalReset = 0.5f;
    private float damageInterval = 0;

    private bool facingRight = true;
    private bool canInflictDamage = true;

    private Transform playerPos;

    private float enemyRot;

    private Vector3 currentPos;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();
        PlayerMask = LayerMask.GetMask("Player");

        SPAWNER = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        SpawnerMask = LayerMask.GetMask("Spawner");

        rb = GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
    }

    private void Start() {
        // anim.SetBool("flying", true);

    }

    private void FixedUpdate() {
        // Vector3 difference = playerPos.position - rb.transform.position;
        // difference.Normalize();
        // enemyRot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // rb.transform.rotation = Quaternion.Euler(0f, 0f, enemyRot + offset);

        // rb.AddForce(transform.up * Speed);

        currentPos = transform.position;

        if (CheckForPlayer()) {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, Speed * Time.deltaTime);

            if (facingRight == false && playerPos.position.x > transform.position.x) {
                Flip();
            }else if (facingRight == true && playerPos.position.x < transform.position.x) {
                Flip();
            }
        } else if (!CheckForSpawner()) {
            Relocate();
        } else {
            Debug.Log("No Player Found");
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

    public void TakeDamage(int damage) {
        Health -= damage;
        anim.SetTrigger("Hit");
        playerScript.damageDealt += damage;

        if (Health <= 0) {
            Die();
        }
    }

    public void Relocate() {
        transform.position = new Vector3(Random.Range(-30f, 30f) + (playerPos.position.x), Random.Range(-30f, 30f) + (playerPos.position.y), 0);
    }

    void Die() {
        // anim.SetBool("dead", true);
        // anim.SetBool("flying", false);
        DestroyObject();
        SpawnBuff(currentPos, DamageUp);
    }

    void SpawnBuff(Vector3 position, GameObject buff) {
        GameObject newBuff = Instantiate(buff, position, Quaternion.identity);
    }

    public void DestroyObject() {
        Destroy(gameObject);
        SPAWNER.currentEnemyCount--;
        playerScript.enemiesKilled++;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
