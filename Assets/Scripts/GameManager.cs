using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance; //Creating GameManager singleton

    public float spawnWaitTime = 4;

    //Note: we are adding currency per projectile hit based on the enemy
    public float currency = 0;
    public Text currencyText;

    public Text nextWaveCountdownText;

    public Text waveText;

    public Animator nextWaveCountdownTextAnim;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject); //If there's already another GameManager, destroy this
        }
        nextWaveCountdownTextAnim = nextWaveCountdownText.GetComponentInParent<Animator>();
        currencyText.text = "Currency: " + currency;
    }

    private void Update()
    {
        nextWaveCountdownText.text = "Next wave: " + WaveSpawner.instance.nextWaveCountdown.ToString("F2");
        waveText.text = WaveSpawner.instance.waves[WaveSpawner.waveIndex].name;

        if(WaveSpawner.instance.nextWaveCountdown <= 0)
        {
            nextWaveCountdownTextAnim.SetTrigger("FadeOut");
        }
        else if (WaveSpawner.instance.nextWaveCountdown >= 0)
        {
            nextWaveCountdownTextAnim.SetTrigger("FadeIn");
        }

    }



    public void AddCurrency(string name)
    {
        int ID = 0;
        if (name.Contains("Blue"))
            ID = 1;
        else if (name.Contains("Green"))
            ID = 2;
        else if (name.Contains("Purple"))
            ID = 3;


        switch (ID)
        {
            case 1:
                currency += 1;
                break;
            case 2:
                currency += 1;
                break;
            case 3:
                currency += 3;
                break;
            default:
                break;
        }

        currencyText.text = "Currency: " + currency;

    }


    /*IEnumerator Start() {
        while (enabled) {
            yield return new WaitForSeconds(spawnWaitTime);
            for (int i = 0; i < 1; i++) {
                int nextEnemyPoint = Random.Range(0, 10);
                Vector3[] points = new Vector3[] {
                    new Vector3(-13.61f, 7.67f, 0),
                    new Vector3(-8.64f, 9.97f, 0),
                    new Vector3(0.45f, 8.16f, 0),
                    new Vector3(12.83f, 7.75f, 0),
                    new Vector3(13.04f, -2.27f, 0),
                    new Vector3(13.41f, -7.1f, 0),
                    new Vector3(5.18f, -7.81f, 0),
                    new Vector3(-0.58f, -7.8f, 0),
                    new Vector3(-9.83f, -8.02f, 0),
                    new Vector3(-13.03f, -1.69f, 0)
                };
                EnemySpawner.instance.BlueEnemySpawn(points[nextEnemyPoint]);
                //EnemySpawner.instance.GreenEnemySpawn(points[nextEnemyPoint]);
                //EnemySpawner.instance.PurpleEnemySpawn(points[nextEnemyPoint]);
            }
        }
    }*/
}
