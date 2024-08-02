using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNecromancer : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject minion;

    [SerializeField]
    private List<GameObject> summonedMinions = new List<GameObject>();

    public string Name;

    public GameObject player;
    public Player playerScript;
    private Transform playerPos;

    public LayerMask PlayerMask;
    public LayerMask PlayerHitDetectMask;

    public Vector3 currentPos;

    public Transform attackPos;
    public Transform summonPos;

    private float Speed = 0;

    public float aggroRange = 10f;
    public float WalkSpeed = 4f;
    public float WalkBackSpeed = 1.25f;

    public float speedLerpDuration = 1f;

    public int Damage = 15;
    public float AttackDelay = 1f;
    public float hitRange = 3f;
    public float FirstSummonDelay = 0.5f;
    public float SummonDelay = 5f;

    private int TotalMinions = 0;

    private bool facingRight = true;
    private bool inSweetSpot;
    private bool isDead = false;

    private float dist;

    void Awake()
    {

        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();
        playerPos = player.GetComponent<Transform>();

        PlayerMask = LayerMask.GetMask("Player");
        PlayerHitDetectMask = LayerMask.GetMask("PlayerHitDetector");

    }

    private void Start() {
        StartCoroutine(AttackCoroutine(AttackDelay));
        StartCoroutine(FirstSummonCoroutine(FirstSummonDelay));
    }

    void FixedUpdate() {
        currentPos = transform.position;

        dist = Vector3.Distance(transform.position, playerPos.position);

        if (!isDead) {

            if (CheckForPlayer()) {


                if (dist > 7f) {
                    StartCoroutine(LerpSpeed(WalkSpeed));
                    anim.SetBool("Walk", true);
                } else if (dist < 5f && dist > 2f) {
                    StartCoroutine(LerpSpeed(WalkBackSpeed));
                    anim.SetBool("Walk", true);
                } else if (dist < 2f) {
                    StartCoroutine(LerpSpeed(-WalkBackSpeed / 1.5f));
                    anim.SetBool("Walk", true);
                }

                if (dist >= 5f && dist <= 7f && inSweetSpot == false) {
                    StartCoroutine(LerpSpeed(0f));
                    anim.SetBool("Walk", false);
                    inSweetSpot = true;
                } else if (dist < 5f && inSweetSpot == true || dist > 7f && inSweetSpot == true) {
                    inSweetSpot = false;
                }

                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, Speed * Time.deltaTime);

                if (facingRight == false && playerPos.position.x > transform.position.x) {
                    Flip();
                } else if (facingRight == true && playerPos.position.x < transform.position.x) {
                    Flip();
                }
            }

        } 
    }

    private IEnumerator AttackCoroutine(float interval) {
        yield return new WaitForSeconds(interval);

        if (!isDead) {
            if (CheckIfPlayerCanBeHit()) {
                anim.SetTrigger("Attack");
            }

            StartCoroutine(AttackCoroutine(AttackDelay));
        }
    }

    private IEnumerator FirstSummonCoroutine(float interval) {
        yield return new WaitForSeconds(interval);

        if (!isDead) {

            if (inSweetSpot) {
                anim.SetTrigger("Summon");
                StartCoroutine(SummonCoroutine(SummonDelay));
            } else {
                StartCoroutine(FirstSummonCoroutine(FirstSummonDelay));
            }

        }

    }

    private IEnumerator SummonCoroutine(float interval) {
        yield return new WaitForSeconds(interval);

        if (!isDead) {

            if (inSweetSpot && TotalMinions < 5) {
                anim.SetTrigger("Summon");
                ResurrectMinions();
            } else if (inSweetSpot && CheckIfMinionsAreDead()) {
                anim.SetTrigger("Resurrect");
            }

            StartCoroutine(SummonCoroutine(SummonDelay));

        }
    }

    void SpawnMinion() {
        GameObject newMinion = Instantiate(minion, summonPos.position, Quaternion.identity);
        summonedMinions.Add(newMinion);
        TotalMinions++;
    }

    bool CheckIfMinionsAreDead() {
        for (int i = 0; i < summonedMinions.Count; i++) {

            Enemy minion = summonedMinions[i].GetComponent<Enemy>();

            if (minion.Dead) {
                return true;
            }
        }
        return false;
    }

    public void ResurrectMinions() {

        for (int i = 0; i < summonedMinions.Count; i++) {

            Enemy minion = summonedMinions[i].GetComponent<Enemy>();

            MiniZombie zombScript = summonedMinions[i].GetComponent<MiniZombie>();

            if (minion.Dead && zombScript.timesResurrected < 3) {
                zombScript.Resurrect();
            } else if (zombScript.timesResurrected == 3) {
                summonedMinions.Remove(summonedMinions[i]);
                TotalMinions--;
            }
        }

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

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.DrawWireSphere(attackPos.position, hitRange);
    }
}
