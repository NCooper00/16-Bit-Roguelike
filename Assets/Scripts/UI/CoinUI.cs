using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{

    private GameObject player;
    private Player playerScript;

    public AudioManager audio;

    public Text text;

    // Start is called before the first frame update
    void Awake()
    {
        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        player = GameObject.Find("PLAYER");
        playerScript = player.GetComponent<Player>();


    }

    public void UpdateCoins(int Coin) {
        text.text = Coin.ToString();
    }
}
