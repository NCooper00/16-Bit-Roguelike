using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniZombie : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;

    private Enemy enemy;

    [SerializeField]
    private GameObject DeathDrop;

    public string Name;

    public GameObject player;
    public Player playerScript;
    private Transform playerPos;

    public LayerMask PlayerMask;
    public LayerMask PlayerHitDetectMask;

    public Vector3 currentPos;

    public Transform attackPos;

    public int Health = 100;

    private float Speed = 0;

    public float aggroRange = 10f;
    public float WalkSpeed = 4f;

    public float speedLerpDuration = 1f;

    public int Damage = 15;
    public float AttackDelay = 1f;
    public float hitRange = 3f;

    public int timesResurrected = 0;

    private bool facingRight = true;
    private bool isDead = false;

    private float dist;

    void Awake()
    {

        enemy = GetComponent<Enemy>();

        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();
        playerPos = player.GetComponent<Transform>();

        PlayerMask = LayerMask.GetMask("Player");
        PlayerHitDetectMask = LayerMask.GetMask("PlayerHitDetector");

    }

    private void Start() {
        StartCoroutine(AttackCoroutine(AttackDelay));
    }

    void FixedUpdate() {
        currentPos = transform.position;

        dist = Vector3.Distance(transform.position, playerPos.position);

        if (!isDead) {

            if (CheckForPlayer()) {

                if (anim.GetBool("Spawned")) {
                    StartCoroutine(LerpSpeed(WalkSpeed));
                }

                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, Speed * Time.deltaTime);

                anim.SetBool("Walk", true);

                if (facingRight == false && playerPos.position.x > transform.position.x) {
                    Flip();
                }else if (facingRight == true && playerPos.position.x < transform.position.x) {
                    Flip();
                }
            } else {
                StartCoroutine(LerpSpeed(0f));
            }

        }
    }

    public void SetSpawned() {
        anim.SetBool("Spawned", true);
    }

    private IEnumerator AttackCoroutine(float interval) {
        yield return new WaitForSeconds(interval);

        if (!isDead) {

            if (CheckIfPlayerCanBeHit()) {
                anim.SetTrigger("Attack");
            }

        }


        StartCoroutine(AttackCoroutine(AttackDelay));
    }

    IEnumerator LerpSpeed(float speed) {
        float timeElapsed = 0;
        float startValue = Speed;
        while (timeElapsed < speedLerpDuration)
        {
            Speed = Mathf.Lerp(startValue, speed, timeElapsed / speedLerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Speed = speed;
    }

    public bool CheckForPlayer() {
        return Physics2D.OverlapCircle(transform.position, aggroRange, PlayerMask);
    }

    public bool CheckIfPlayerCanBeHit() {
        return Physics2D.OverlapCircle(attackPos.position, hitRange, PlayerHitDetectMask);
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

    public void Dead() {
        Speed = 0f;
        isDead = true;
    }

    public void Resurrect() {
        anim.SetTrigger("Resurrect");
        enemy.Health = enemy.maxHealth / (1 + timesResurrected);
    }

    public void ResurrectFinished() {
        enemy.Dead = false;
        anim.SetBool("Dead", false);
        enemy.ToggleCollider();
        isDead = false;

        timesResurrected++;
        anim.SetInteger("Resurrected", timesResurrected);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.DrawWireSphere(attackPos.position, hitRange);
    }
}
