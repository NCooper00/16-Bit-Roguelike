using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private int MaxHealth = 1000;
    private int Health = 1000;

    public int enemiesKilled;
    public int damageDealt;
    public int damageTaken;
    public int consumablesCollected;
    public int Coins = 0;

    public int weaponDamage = 10;

    public float moveSpeed = 4f;
    private float moveSpeedReset;
    public float sprintMultiplier = 1.25f;

    private float directionX;
    private float directionY;

    private bool facingRight = true;

    public Rigidbody2D rb;

    public Animator anim;
    public AudioManager audio;

    private GameObject coinUI;
    private CoinUI coinUIScript;

    void Awake() {
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        coinUI = GameObject.Find("Coin");
        coinUIScript = coinUI.GetComponent<CoinUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedReset = moveSpeed;

        enemiesKilled = 0;
        damageDealt = 0;
        damageTaken = 0;
        consumablesCollected = 0;

        Coins = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            moveSpeed = moveSpeed * sprintMultiplier;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            moveSpeed = moveSpeedReset;
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate() {
        directionX = Input.GetAxis("Horizontal");
        directionY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveSpeed * directionX, moveSpeed * directionY);

        // if (rb.velocity.x != 0) {
        //     anim.SetBool("Move", true);
        // } else {
        //     anim.SetBool("Move", false);
        // }

        if (directionX != 0 || directionY != 0) {
            anim.SetBool("Run", true);
        } else if (directionX == 0 && directionY == 0) {
            anim.SetBool("Run", false);
        }

        if (facingRight == false && directionX > 0) {
            Flip();
        }else if (facingRight == true && directionX < 0) {
            Flip();
        }
    }

    void Flip() {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    public void IncreaseDamage(int Damage) {
        weaponDamage += Damage;
    }

    public void IncreaseCoin(int Coin) {
        Coins += Coin;
        coinUIScript.UpdateCoins(Coins);
    }

    public void TakeDamage(int damage) {
        Health -= damage;
        anim.SetTrigger("Hit");

        damageTaken += damage;
        // CameraShaker.Instance.ShakeOnce(4f, 2f, .1f, 1f);

        if (Health <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Am Ded");
    }

    public int ReturnHealth() {
        return Health;
    }

    public void PlayStepSound() {
        audio.PlayFull("PlayerStep");
    }
}
