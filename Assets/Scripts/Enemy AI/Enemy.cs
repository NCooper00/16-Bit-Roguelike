using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;

    [HideInInspector]
    public int maxHealth;
    public int Health = 100;

    [SerializeField]
    private GameObject DeathDrop;

    private Collider2D collider;

    private GameObject player;
    private Player playerScript;
    private Transform playerPos;

    public bool Dead = false;

    public Vector3 currentPos;

    void Awake()
    {

        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();
        playerPos = player.GetComponent<Transform>();

    }

    void Start() {
        collider = GetComponent<Collider2D>();

        maxHealth = Health;
    }

    void FixedUpdate() {
        currentPos = transform.position;
    }

    public void TakeDamage(int damage) {
        Health -= damage;
        anim.SetTrigger("Hit");
        playerScript.damageDealt += damage;

        if (Health <= 0) {
            Die();
        }
    }

    void Die() {
        Health = 0;
        // SpawnBuff(currentPos, Coin);
        Dead = true;
        anim.SetBool("Dead", true);
        anim.SetTrigger("Die");
        ToggleCollider();
    }

    public void Bleed() {
        SpawnDeathDrop(currentPos, DeathDrop);
    }

    public void ToggleCollider() {
        collider.enabled = !collider.enabled;
    }

    void SpawnDeathDrop(Vector3 position, GameObject deathDrop) {
        GameObject newDeathDrop = Instantiate(deathDrop, position, Quaternion.identity);
    }

    void SpawnBuff(Vector3 position, GameObject buff) {
        GameObject newBuff = Instantiate(buff, position, Quaternion.identity);
    }

    public void DestroyObject() {
        playerScript.enemiesKilled++;
        Destroy(gameObject);
    }
}