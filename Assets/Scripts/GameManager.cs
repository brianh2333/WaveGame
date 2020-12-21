using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance; //Creating GameManager singleton

    [Header("---VARIABLES---")]
    public float spawnWaitTime = 4;
    //Note: we are adding currency per projectile hit based on the enemy
    public float currency = 0;
    bool isPaused = false;
    public bool shopMenuOpen = false;

    [Header("---TEXTS---")]
    public Text nextWaveCountdownText;
    public Text waveText;
    public Text currencyText;

    [Header("---GAMEOBJECTS---")]
    public Animator nextWaveCountdownTextAnim;
    public Animator pauseButtonAnim;
    public Animator shopMenuAnimator;
    public GameObject pauseMenu;
    public GameObject buttonPanel;
    public GameObject optionsPanel;
    public GameObject instructionsPanel;
    public GameObject pauseMenuButton;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject); //If there's already another GameManager, destroy this
        }
        nextWaveCountdownTextAnim = nextWaveCountdownText.GetComponentInParent<Animator>();
        currencyText.text = "Currency: " + currency;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        nextWaveCountdownText.text = "Next wave: " + WaveSpawner.instance.nextWaveCountdown.ToString("F2");
        waveText.text = WaveSpawner.instance.waves[WaveSpawner.waveIndex].name;

        if (WaveSpawner.instance.nextWaveCountdown <= 0)
        {
            nextWaveCountdownTextAnim.SetTrigger("FadeOut");
        }
        else if (WaveSpawner.instance.nextWaveCountdown >= 0)
        {
            nextWaveCountdownTextAnim.SetTrigger("FadeIn");
        }

        currencyText.text = "Currency: " + currency;

        //PAUSE MENU
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPaused = true;
            pauseButtonAnim.SetBool("Pause", isPaused);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPaused = false;
            pauseButtonAnim.SetBool("Pause", isPaused);
        }
    }

    public void PauseMenuButton()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPaused = true;
            pauseButtonAnim.SetBool("Pause", isPaused);
        }
        else if (isPaused)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPaused = false;
            pauseButtonAnim.SetBool("Pause", isPaused);
        }
    }

    public void PauseMenuButtonEnter()
    {
        pauseMenuButton.GetComponentInChildren<Animator>().SetBool("Hover", true);
    }

    public void PauseMenuButtonExit()
    {
        pauseMenuButton.GetComponentInChildren<Animator>().SetBool("Hover", false);
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPaused = false;
            pauseButtonAnim.SetBool("Pause", isPaused);
        }
    }

    public void OptionsButton()
    {
        if (isPaused)
        {
            buttonPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }
    }

    public void OptionsBackButton()
    {
        if (isPaused)
        {
            buttonPanel.SetActive(true);
            optionsPanel.SetActive(false);
        }
    }

    public void InstructionsButton()
    {
        if (isPaused)
        {
            buttonPanel.SetActive(false);
            instructionsPanel.SetActive(true);
        }
    }

    public void InstructionsBackButton()
    {
        if (isPaused)
        {
            buttonPanel.SetActive(true);
            instructionsPanel.SetActive(false);
        }
    }

    public void MainMenuButton()
    {
        if (isPaused)
        {
            SceneManager.LoadScene(0);
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
        else if (name.Contains("Red"))
            ID = 4;


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
            case 4:
                currency += 4;
                break;
            default:
                break;
        }
        currencyText.text = "Currency: " + currency;
    }

    public void ShopMenuHover() {
        if (shopMenuOpen == false) {
            shopMenuAnimator.SetBool("SliderAnim", true);
            shopMenuOpen = true;
            ShopMenuOpen();
        }
    }
    
    public void ShopMenuExit() {
        if (shopMenuOpen == true) {
            shopMenuOpen = false;
            shopMenuAnimator.SetBool("SliderAnim", false);
        }
    }

    public void ShopMenuOpen() {
        shopMenuOpen = true;
    }

    public void ShopMenuClose() {
        shopMenuOpen = true;
        ShopMenuExit();
    }
}
