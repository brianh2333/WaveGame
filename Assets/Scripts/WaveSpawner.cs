using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum WaveStatus
    {
        COUNTING,
        SPAWNING,
        WAITING,
        COMPLETE
    }

    public ObjectPooler objPooler;

    public WaveStatus status;// = WaveStatus.WAITING;

    public GlobalTimer globalTime;

    public ModifyTimer mTime;

    private static int waveIndex = 0;
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
    private bool spawnWave;
    private bool calculateSpawn = true;
    private float nextSpawn = 0;

    //[SerializeField]
    public Wave[] waves;

    private void Start()
    {
        objPooler = ObjectPooler.Instance;
        spawnWave = true;
    }

    private void Awake()
    {
        countEnemies = true;
        status = WaveStatus.COUNTING;
        enemyID = Random.Range(0, waves[0].enemies.Length);
        mTime = GetComponent<ModifyTimer>();
    }

    private void Update()
    {
        if (waveIndex < waves.Length)
        {
            currentWave = waves[waveIndex].name;

            if (status == WaveStatus.COUNTING && countEnemies)
            {
                enemiesToSpawn = 0;
                for (int i = 0; i < waves[waveIndex].enemies.Length; i++)
                {
                    enemiesToSpawn += waves[waveIndex].enemies[i].amount;
                }
                countEnemies = false;
                Debug.Log(enemiesToSpawn + " to spawn");

            }

            if ((globalTime.timer >= 0 && status == WaveStatus.SPAWNING) || (spawnWave && status == WaveStatus.SPAWNING))
            {
                float timeBetweenEnemies = 5f;
                if (spawnWave)
                {
                    timeBetweenEnemies = 0f;
                    spawnWave = false;
                }


                if (calculateSpawn)
                {
                    nextSpawn = globalTime.timer - timeBetweenEnemies;
                    calculateSpawn = false;
                }
                if ((globalTime.timer <= nextSpawn && enemyIndex < enemiesToSpawn) || spawnWave)
                {
                    status = WaveStatus.SPAWNING;
                    objPooler.SpawnFromPool(waves[waveIndex].enemies[enemyID].name);
                    enemyID = Random.Range(0, waves[waveIndex].enemies.Length);
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
            else if ((aliveEnemies == 0 || globalTime.timer == 0) && status == WaveStatus.WAITING)
            {
                waveIndex++;
                status = WaveStatus.COMPLETE;
            }


            if (status == WaveStatus.COMPLETE)
            {
                Debug.Log("Wave complete!");
                spawnWave = true;
                countEnemies = true;
            }
        }
    }
}
