using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance; //Creating GameManager singleton

    [Header("---VARIABLES---")]
    public float spawnWaitTime = 4;
    //Note: we are adding currency per projectile hit based on the enemy
    public float currency = 0;
    
    [Header("---TEXTS---")]
    public Text nextWaveCountdownText;
    public Text waveText;
    Text currencyText;

    [Header("---GAMEOBJECTS---")]
    public Animator nextWaveCountdownTextAnim;
    public GameObject pauseMenu;
    public GameObject shopMenu;

    void Awake() {
        if (instance == null) instance = this;
        else {
            Destroy(this.gameObject); //If there's already another GameManager, destroy this
        }
        nextWaveCountdownTextAnim = nextWaveCountdownText.GetComponentInParent<Animator>();
        currencyText.text = "Currency: " + currency;
        shopMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void Update() {
        nextWaveCountdownText.text = "Next wave: " + WaveSpawner.instance.nextWaveCountdown.ToString("F2");
        waveText.text = WaveSpawner.instance.waves[WaveSpawner.waveIndex].name;

        if(WaveSpawner.instance.nextWaveCountdown <= 0) {
            nextWaveCountdownTextAnim.SetTrigger("FadeOut");
        }
        else if (WaveSpawner.instance.nextWaveCountdown >= 0) {
            nextWaveCountdownTextAnim.SetTrigger("FadeIn");
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenu();
        }
    }

    public void AddCurrency(string name) {
        int ID = 0;
        if (name.Contains("Blue"))
            ID = 1;
        else if (name.Contains("Green"))
            ID = 2;
        else if (name.Contains("Purple"))
            ID = 3;


        switch (ID) {
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

    public void PauseMenu() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
