using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance; //Singleton

    public float waveTimer; //countdown until current wave ends
    public float nextWaveCountdown; //countdown inbetween waves
    
    //we need to store the above 2 values to reset them at the end of each wave
    private float waveTimerSaved; 
    private float nextWaveCountdownSaved;


    public enum WaveStatus
    {
        COUNTING,
        SPAWNING,
        WAITING,
        COMPLETE
    }

    public Transform[] enemySpawns;

    public WaveStatus status;


    public static int waveIndex = 0;
    private static int enemyIndex = 0;
    private static int enemyID;


    public static int aliveEnemies = 0;

    private float enemiesToSpawn = 0;

    [System.Serializable]
    public class Wave
    {
        public string name;
        [System.Serializable]
        public class Enemies
        {
            public string name;
            public int amount;
        }
        public Enemies[] enemies;
    }

    public static string currentWave;

    private bool countEnemies;
    private bool spawnWave = true;
    private bool calculateSpawn = false;
    private float nextSpawn = 0;

    public Wave[] waves;

    private void Start()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        for (int i = 0; i < spawns.Length; i++){
            enemySpawns[i] = spawns[i].transform;
        }


        status = WaveStatus.COUNTING;
        enemiesToSpawn = 0;

        waveTimerSaved = waveTimer;
        nextWaveCountdownSaved = nextWaveCountdown;

        countEnemies = true;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (waveIndex < waves.Length && waves.Length > 0)
        {
            if(nextWaveCountdown <= 0)
            {
                currentWave = waves[waveIndex].name;

                if (status == WaveStatus.COUNTING && countEnemies)
                {
                    enemiesToSpawn = 0;
                    for (int i = 0; i < waves[waveIndex].enemies.Length; i++)
                    {
                        enemiesToSpawn += waves[waveIndex].enemies[i].amount;
                    }
                    status = WaveStatus.SPAWNING;
                    countEnemies = false;
                    Debug.Log(enemiesToSpawn + " to spawn");

                }

                else if ((waveTimer >= 0 && status == WaveStatus.SPAWNING) || (spawnWave && status == WaveStatus.SPAWNING) || spawnWave)
                {
                    float timeBetweenEnemies = 5f;
                    if (spawnWave)
                    {
                        calculateSpawn = true;
                        
                        timeBetweenEnemies = 0f;
                        spawnWave = false;
                    }

                    if (calculateSpawn)
                    {
                        nextSpawn = waveTimer - timeBetweenEnemies;
                        calculateSpawn = false;
                    }
                    if (waveTimer <= nextSpawn && enemyIndex < enemiesToSpawn)
                    {
                        enemyID = Random.Range(0, waves[waveIndex].enemies.Length);
                        SpawnEnemy(enemyID);

                        aliveEnemies++;
                        enemyIndex++;
                        Debug.Log("Spawning enemy...");
                        calculateSpawn = true;
                    }
                    else if (enemyIndex == enemiesToSpawn)
                    {
                        calculateSpawn = true;
                        enemyIndex = 0;
                        status = WaveStatus.WAITING;
                    }
                }
                else if ((aliveEnemies == 0 || waveTimer == 0) && status == WaveStatus.WAITING)
                {
                    waveIndex++;
                    status = WaveStatus.COMPLETE;

                }


                if (status == WaveStatus.COMPLETE)
                {
                    waveTimer = waveTimerSaved; //reset time;
                    nextWaveCountdown = nextWaveCountdownSaved;
                    status = WaveStatus.COUNTING;
                    Debug.Log("Wave complete!");
                    spawnWave = true;
                    countEnemies = true;
                }

                if (status == WaveStatus.WAITING || status == WaveStatus.SPAWNING )
                    waveTimer -= Time.deltaTime;

            }
            else if (nextWaveCountdown >= 0 && status == WaveStatus.COUNTING)
            {
                nextWaveCountdown -= Time.deltaTime;
            }
        }
    }


    void SpawnEnemy(int ID)
    {
        Vector2 spawnPosition = enemySpawns[Random.Range(0, enemySpawns.Length)].position; //get random spawn position

        switch (ID)
        {
            case 0:
                EnemySpawner.instance.BlueEnemySpawn(spawnPosition);
                break;
            case 1:
                EnemySpawner.instance.GreenEnemySpawn(spawnPosition);
                break;
            case 2:
                EnemySpawner.instance.PurpleEnemySpawn(spawnPosition);
                break;
            case 3:
                EnemySpawner.instance.RedEnemySpawn(spawnPosition);
                break;
            default:
                Debug.LogWarning("No such enemy with ID: " + ID);
                break;
        }
    }
}
