using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    private Player player;

    public float FiringMoveSpeed = 1.5f;

    public Transform firePoint;

    public GameObject bulletPrefab;
    public AudioManager audio;
    
    void Awake() {
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

    }

    void FixedUpdate() {

        if (Input.GetMouseButton(0) && !player.Attacking) {
            BeginAttack();
        }

    }

    void BeginAttack() {
            player.ChangeSpeed(FiringMoveSpeed);
            player.Attacking = true;
            player.anim.SetTrigger("Attacking");
    }

    public void Attack() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
