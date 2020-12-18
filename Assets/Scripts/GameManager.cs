using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Wave counter
    public Text waveText;

    //Time until next wave
    public WaveSpawner spawner;
    public ModifyTimer timer;
    public GlobalTimer globalTimer;
    public Text countdownText;
    public Animator countdownAnim;
    public GameObject countdown;
    private float counttime = 0;

    //Currency display
    public int currency;
    private int savedCurrency = 0;
    public Text currencyText;

    public HealthSystem healthSystem; //player health
    private int currentLevel;
    private string sceneName;

    void Start()
    {
        countdownAnim = countdown.GetComponent<Animator>();
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>(); //player health;
        sceneName = SceneManager.GetActiveScene().name;
        currentLevel = SceneManager.GetActiveScene().buildIndex; //will be 0 until start menu is added
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = WaveSpawner.currentWave;

        if (globalTimer.nextWave > 0 && spawner.status == WaveSpawner.WaveStatus.COUNTING)
        {
            counttime = globalTimer.nextWave;
            countdown.SetActive(true);
            countdownAnim.SetTrigger("FadeIn");
            countdownText.text = "Next wave: \n" + counttime.ToString("F2");
        }
        else
            StartCoroutine(FadeOut());

        if (savedCurrency != currency)
        {
            savedCurrency = currency;
            currencyText.text = "Currency: " + currency.ToString("D");

        }

        if (savedCurrency == 0)
        {
            currencyText.text = "Currency: " + savedCurrency.ToString("D");
        }
        currency = savedCurrency;

        
        //Reload level
        if(healthSystem.GetCurrentHealth() < 0)
        {
            ReloadLevel();
        }


    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }



    private IEnumerator FadeOut()
    {
        countdownAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        countdown.SetActive(false);
    }

    public void AddCurrency(string name)
    {
        int ID = 0;
        if (name.Contains("BlueCube"))
            ID = 1;
        else if (name.Contains("GreenCube"))
            ID = 2;
        else if (name.Contains("PurpleCube"))
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

    }
}
