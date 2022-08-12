using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameObject player;
    private Player playerScript;

    [SerializeField]
    private float bulletLife = 3f;

    public float bulletSpeed = 5f;
    private float bulletLifeReset;
    

    private int bulletDamage;

    public Rigidbody2D rb;
    public Animator anim;

    // public AudioManager audio;

    
    void Awake() {
        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();

        bulletDamage = playerScript.weaponDamage;
        // audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        bulletLifeReset += bulletLife;
    }

    void Start()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    void FixedUpdate() {
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0) {
            DestroyBullet();
            bulletLife += bulletLifeReset;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(bulletDamage);
        }
        
        if (hitInfo.gameObject.layer != 6 && hitInfo.gameObject.layer != 8) {
            anim.SetTrigger("hit");
            rb.velocity = new Vector2(0f, 0f);
            // DestroyBullet();
            // audio.PlayFull("TurretShotImpact");
            bulletDamage = 0;
        }

    }

    public void DestroyBullet() {
        Destroy(gameObject);
    }
}
