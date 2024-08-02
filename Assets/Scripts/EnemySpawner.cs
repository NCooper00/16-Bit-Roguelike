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

    private Vector3 SpawnLocation;
    private Vector3 RelocateLocation;

    public Collider2D playerZoneCollider;
    public Collider2D outerCollider;

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

    [SerializeField]
    private GameObject reaperPrefab;
    [SerializeField]
    private float reaperInterval = 300f;

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
        StartCoroutine(spawnEnemy(reaperInterval, reaperPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxEnemyReached && currentEnemyCount < maxEnemyCount) {
            
            MaxEnemyReached = false;
            StartCoroutine(spawnEnemy(skeleton1Interval, skeleton1Prefab));
            StartCoroutine(spawnEnemy(vampireInterval, vampirePrefab));
            StartCoroutine(spawnEnemy(reaperInterval, reaperPrefab));
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        FindSpawnLocation(enemy);

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

    void FindSpawnLocation(GameObject enemy) {
        SpawnLocation = new Vector3(Random.Range(-30f, 30f) + (playerPos.position.x), Random.Range(-30f, 30f) + (playerPos.position.y), 0);

        if (!CheckIfTooClose(SpawnLocation)) {
            EnemySpawn(SpawnLocation, enemy);
        } else {
            FindSpawnLocation(enemy);
        }
    }

    bool CheckIfTooClose(Vector3 newPos) {
        if (playerZoneCollider.bounds.Contains(newPos)) {
            return true;
        } else {
            return false;
        }
    }

    void EnemySpawn(Vector3 spawnSpot, GameObject enemy) {
        GameObject newEnemy = Instantiate(enemy, SpawnLocation, Quaternion.identity);
        currentEnemyCount++;
    }

    void FindRelocateLocation(Enemy enemy) {
        RelocateLocation = new Vector3(Random.Range(-30f, 30f) + (playerPos.position.x), Random.Range(-30f, 30f) + (playerPos.position.y), 0);

        if (!CheckIfTooClose(RelocateLocation)) {
            EnemyRelocate(RelocateLocation, enemy);
        } else {
            FindRelocateLocation(enemy);
        }
    }

    void EnemyRelocate(Vector3 relocateSpot, Enemy enemy) {

        enemy.currentPos = relocateSpot;

    }

    void OnTriggerExit2D(Collider2D objectInfo) {
        if (objectInfo.GetComponent<Collider>() == outerCollider) {

            Enemy enemy = objectInfo.GetComponent<Enemy>();

            if (enemy != null) {
                FindRelocateLocation(enemy);
            }

        }

    }

}
