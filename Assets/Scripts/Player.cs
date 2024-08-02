using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float MaxHealth = 1000;
    [SerializeField]
    private float Health = 1000;

    public int enemiesKilled;
    public float damageDealt;
    public float damageTaken;
    public int consumablesCollected;
    public int Coins = 0;

    public int weaponDamage = 10;

    public float moveSpeed = 4f;
    public float sprintSpeed = 6f;
    public float gotHitSpeed = 2f;
    private float moveSpeedReset;
    public float currentSpeed;

    public float gotHitSpeedDelay = 1.5f;
    private float gotHitSpeedDelayReset;

    private float directionX;
    private float directionY;

    private bool facingRight = true;
    public bool Attacking = false;
    public bool gotHit = false;

    public Rigidbody2D rb;

    public Animator anim;
    public AudioManager audio;

    public GameObject coinUI;
    private CoinUI coinUIScript;

    [SerializeField]
    private Weapon weapon;

    void Awake() {
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        coinUIScript = coinUI.GetComponent<CoinUI>();

        gotHitSpeedDelayReset = gotHitSpeedDelay;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;

        enemiesKilled = 0;
        damageDealt = 0;
        damageTaken = 0;
        consumablesCollected = 0;

        Coins = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (!Attacking && !gotHit) {
                ChangeSpeed(sprintSpeed);
            }
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            if (!Attacking && !gotHit) {
                ChangeSpeed(moveSpeed);
            }
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate() {
        directionX = Input.GetAxis("Horizontal");
        directionY = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(directionX, directionY);

        if (moveDir.sqrMagnitude > 1) {
            moveDir.Normalize();
        }

        rb.velocity = moveDir * currentSpeed;
        // rb.velocity = new Vector2(currentSpeed * directionX, currentSpeed * directionY).normalized;

        if (gotHit) {
            ChangeSpeed(gotHitSpeed);

            gotHitSpeedDelay -= Time.deltaTime;

            if (gotHitSpeedDelay <= 0) {
                gotHit = false;
                gotHitSpeedDelay = gotHitSpeedDelayReset;
            }
        }

        if (directionX != 0 || directionY != 0) {
            anim.SetBool("Run", true);
        } else if (directionX == 0 && directionY == 0) {
            anim.SetBool("Run", false);
        }

        if (!Attacking) {
            if (!facingRight && directionX > 0) {
                Flip();
            } else if (facingRight && directionX < 0) {
                Flip();
            }
        } else if (Attacking) {
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (!facingRight && Input.mousePosition.x > playerScreenPos.x) {
                Flip();
            } else if (facingRight && Input.mousePosition.x < playerScreenPos.x) {
                Flip();
            }
        }
    }

    public void ChangeSpeed(float speed) {
        currentSpeed = speed;
    }

    void Flip() {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    public void Attack() {
        weapon.Attack();
    }

    public void StopAttack() {
        Attacking = false;
        ChangeSpeed(moveSpeed);
    }

    public void IncreaseDamage(int Damage) {
        weaponDamage += Damage;
    }

    public void IncreaseCoin(int Coin) {
        Coins += Coin;
        coinUIScript.UpdateCoins(Coins);
    }

    public void TakeDamage(AttackDetails attackDetails) {

        if (Attacking) {
            Attacking = false;
        }

        Health -= attackDetails.damageAmount;
        anim.SetTrigger("Hit");

        damageTaken += attackDetails.damageAmount;

        if (Health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Am Ded");
    }

    public float ReturnHealth() {
        return Health;
    }

    public void PlayStepSound() {
        audio.PlayFull("PlayerStep");
    }

}
