using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;

    public GameObject bulletPrefab;
    // public AudioManager audio;

    // public Animator anim;

    public float fireRate = 0.25f;
    private float fireRateReset;

    private float timeSinceLastShot;

    private float CurrentTime;

    
    void Awake() {
        fireRateReset += fireRate;
        // audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        // hasNotShot = true;

    }

    void Start() {
        timeSinceLastShot = Time.time;
    }

    void FixedUpdate() {

        // if (hasNotShot && Input.GetMouseButton(0)) {
        //     Shoot();
        //     hasNotShot = false;
        // }

        CurrentTime += Time.deltaTime;
        // Debug.Log(timeSinceLastShot);
        Debug.Log(CurrentTime - timeSinceLastShot);

        if (fireRate != 0) {
            fireRate -= Time.deltaTime;
            
            if (fireRate <= 0) {
                fireRate = 0;
            }
        }

        if (Input.GetMouseButton(0) && fireRate == 0) {

            // if (fireRate == fireRateReset) {
                Shoot();
                fireRate = fireRateReset;
            // }
            
        } 
        // else if (!Input.GetMouseButton(0)) {
        
        //     // fireRate = fireRateReset;
        //     // anim.SetBool("shooting", false);
        //     // hasNotShot = true;
        // }
    }

    void Shoot() {
        // audio.Play("TurretShot");
        // anim.SetBool("shooting", true);
        timeSinceLastShot += Time.deltaTime;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
