using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{

    private int Damage = 15;

    private GameObject player;
    private Player playerScript;

    public AudioManager audio;

    // Start is called before the first frame update
    void Awake()
    {
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        Player player = col.gameObject.GetComponent<Player>();

        if (player != null) {
            player.IncreaseDamage(Damage);
            audio.PlayFull("Coin");
            DestroyObject();
        }

    }

    public void DestroyObject() {
        Destroy(gameObject);
        playerScript.consumablesCollected++;
    }
}
