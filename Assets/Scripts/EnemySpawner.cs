using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // [SerializeField]
    private GameObject player;
    private Transform playerPos;

    // Amount of Enemies Multiplier
    private int AOE = 1;
    private int AOEIncrement = 1;
    private float AOEInterval = 30f;

    // Difficulty of Enemies Multiplier
    private int DOE = 1;

    // Frequency of Enemies Multiplier
    private int FOE = 1;

    public int currentEnemyCount;
    public int maxEnemyCount;
    private int maxEnemiesStart;

    public bool MaxEnemyReached;

    [SerializeField]
    private GameObject skeleton1Prefab;
    [SerializeField]
    private float skeleton1Interval = 5f;

    [SerializeField]
    private GameObject vampirePrefab;
    [SerializeField]
    private float vampireInterval = 10f;

    void Awake() {
        player = GameObject.Find("PLAYER");
        playerPos = player.GetComponent<Transform>();
    }

    void Start()
    {
        MaxEnemyReached = false;
        maxEnemiesStart = maxEnemyCount;

        StartCoroutine(AOECoroutine(AOEInterval));

        StartCoroutine(spawnEnemy(skeleton1Interval, skeleton1Prefab));
        StartCoroutine(spawnEnemy(vampireInterval, vampirePrefab));
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxEnemyReached && currentEnemyCount < maxEnemyCount) {
            
            MaxEnemyReached = false;
            StartCoroutine(spawnEnemy(skeleton1Interval, skeleton1Prefab));
            StartCoroutine(spawnEnemy(vampireInterval, vampirePrefab));
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-30f, 30f) + (playerPos.position.x), Random.Range(-30f, 30f) + (playerPos.position.y), 0), Quaternion.identity);
        currentEnemyCount++;

        if (currentEnemyCount <= maxEnemyCount && !MaxEnemyReached) {
            StartCoroutine(spawnEnemy(interval, enemy));
        } else if (currentEnemyCount >= maxEnemyCount) {
            MaxEnemyReached = true;
        }
    }

    private IEnumerator AOECoroutine(float interval) {
        yield return new WaitForSeconds(interval);
        AOE += AOEIncrement;
        maxEnemyCount = maxEnemiesStart * AOE;

        if (AOE <= 9) {
            StartCoroutine(AOECoroutine(interval));
        }
    }

    void OnTriggerExit2D(Collider2D objectInfo) {
        Enemy enemy = objectInfo.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.Relocate();
        }

    }

}
