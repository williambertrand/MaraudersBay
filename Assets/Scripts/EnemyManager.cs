using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    #region Singleton
    public static EnemyManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    #endregion

    [SerializeField] private GameObject player;

    public int currentEnemyCount;
    public int timeBetweenSpawn;

    private int currentSpawnId = 0;
    public List<Enemy> enemies;

    public GameObject enemyPrefab;
    public PlayerReputation playerRep;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;

    [Header("Parameters for enemy spawn strength")]
    public int minSpawnHealth;
    public int maxSpawnHealth;
    public int maxPlayerRep;


    public float bounds;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < currentEnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyDeath(int id, GameObject actor, Vector3 pos)
    {
        Enemy deadEnemy = enemies.Find(e => e.id == id);
        if(deadEnemy == null)
        {
            Debug.Log("Dead enemy was missing in manager list");
            return;
        }

        DropManager.Instance.SpawnChestAt(pos);

        PlayerReputation rep = actor.GetComponent<PlayerReputation>();
        if (rep != null)
        {
            rep.updateReputationBy(deadEnemy.reputationAward);
        }

        enemies.Remove(deadEnemy);

        // TODO: continue to increase spawn count as player rep increases
        int toSpawnCount = playerRep.playerCurrentReputation < 10 ? 1 : 2;
        for (int i = 0; i < toSpawnCount; i++)
        {
            SpawnEnemy();
        }
    }


    public Vector3 GetSampleSpawn()
    {
        float xPos = Random.Range(-bounds, bounds);
        float zPos = Random.Range(-bounds, bounds);
        return new Vector3(xPos, 0.2f, zPos);
    }


    public const float MIN_DIST_PLAYER = 200.0f;
    public const float MIN_DIST_ENEMY = 100.0f;

    public bool validateSpawnLoc(Vector3 spawnLoc)
    {

        RaycastHit hit;
        if (Physics.Raycast(spawnLoc + new Vector3(0, 5f, 0), -Vector3.up, out hit))
        {

            // Check spawn loc is water
            if (!hit.collider.gameObject.CompareTag("Water")) return false;

            // Check spawn loc is not within X dist of player
            if (Physics.OverlapSphere(hit.point, MIN_DIST_PLAYER, playerLayerMask).Length > 0)
            {
                return false;
            }

            // Check spawn loc is not within X dist of other enemy
            if (Physics.OverlapSphere(hit.point, MIN_DIST_ENEMY, enemyLayerMask).Length > 0)
            {
                return false;
            }

            return true;
        }

        // Off map
        return false;
    }

    public int CurrentSpawnHealth()
    {
        return (int) Mathf.Lerp(minSpawnHealth, maxSpawnHealth, playerRep.playerCurrentReputation / maxPlayerRep);
    }

    public void SpawnEnemy()
    {

        int attempts = 1;
        Vector3 posibleSpawn = GetSampleSpawn();

        while(!validateSpawnLoc(posibleSpawn))
        {
            attempts++;
            posibleSpawn = GetSampleSpawn();

            if (attempts > 5)
            {
                Debug.LogError("Could not find suitable loc for enemy spawn");
                return;
            }
        }

        Debug.Log("Got valid spawn loc after attemps: " + attempts);
        GameObject newEnemy = Instantiate(enemyPrefab, posibleSpawn, Quaternion.identity);
        newEnemy.transform.parent = transform;

        Enemy enemyInfo = newEnemy.GetComponent<Enemy>();
        enemyInfo.health = CurrentSpawnHealth();

        // Bit of a hack here to identify enemies
        enemyInfo.id = currentSpawnId;
        currentSpawnId++;

        enemies.Add(enemyInfo);
    }
}