using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    public static EnemySpawner instance; //Creating a EnemySpawner singleton

    public GameObject blueEnemyPrefab;
    public GameObject greenEnemyPrefab;
    public GameObject purpleEnemyPrefab;
    public int poolSize = 50;
    GameObject[] blueEnemyPool;
    GameObject[] greemEnemyPool;
    GameObject[] purpleEnemyPool;
    int currentIndex = 0;

    void Awake() {
        if (instance == null) instance = this;
    }

    void Start() {
        BlueEnemy();
        GreenEnemy();
        PurpleEnemy();
    }

    void BlueEnemy() {
        blueEnemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            GameObject blueEnemy = Instantiate(blueEnemyPrefab);
            blueEnemy.SetActive(false);
            blueEnemyPool[i] = blueEnemy;
        }
    }

    void GreenEnemy() {
        greemEnemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            GameObject greenEnemy = Instantiate(greenEnemyPrefab);
            greenEnemy.SetActive(false);
            greemEnemyPool[i] = greenEnemy;
        }
    }

    void PurpleEnemy() {
        purpleEnemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            GameObject purpleEnemy = Instantiate(purpleEnemyPrefab);
            purpleEnemy.SetActive(false);
            purpleEnemyPool[i] = purpleEnemy;
        }
    }

    public void BlueEnemySpawn(Vector3 pos) {
        GameObject enemy = blueEnemyPool[currentIndex];
        enemy.transform.position = pos;
        enemy.SetActive(true);
        currentIndex++;
        currentIndex %= poolSize;
    }

    public void GreenEnemySpawn(Vector3 pos) {
        GameObject enemy = greemEnemyPool[currentIndex];
        enemy.transform.position = pos;
        enemy.SetActive(true);
        currentIndex++;
        currentIndex %= poolSize;
    }

    public void PurpleEnemySpawn(Vector3 pos) {
        GameObject enemy = purpleEnemyPool[currentIndex];
        enemy.transform.position = pos;
        enemy.SetActive(true);
        currentIndex++;
        currentIndex %= poolSize;
    }
}
