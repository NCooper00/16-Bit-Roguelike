using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;

    public int Health = 100;

    [SerializeField]
    private GameObject player;
    private Player playerScript;
    private Transform playerPos;

    private EnemySpawner SPAWNER;

    [SerializeField]
    private GameObject DamageUp;
    [SerializeField]
    private GameObject Coin;

    private Vector3 currentPos;

    void Awake()
    {
        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();
        playerPos = player.GetComponent<Transform>();

        SPAWNER = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
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
        SpawnBuff(currentPos, Coin);
        DestroyObject();
    }

    void SpawnBuff(Vector3 position, GameObject buff) {
        GameObject newBuff = Instantiate(buff, position, Quaternion.identity);
    }

    public void DestroyObject() {
        SPAWNER.currentEnemyCount--;
        playerScript.enemiesKilled++;
        Destroy(gameObject);
    }

    public void Relocate() {
        transform.position = new Vector3(Random.Range(-30f, 30f) + (playerPos.position.x), Random.Range(-30f, 30f) + (playerPos.position.y), 0);
    }
}