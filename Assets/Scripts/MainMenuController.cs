using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    
    public GameObject middlePanel;
    public GameObject instructionsPanel;
    public GameObject optionsPanel;

    void Start() {
        middlePanel.SetActive(true);
        instructionsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void Instructions() {
        middlePanel.SetActive(false);
        instructionsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void Options() {
        middlePanel.SetActive(false);
        instructionsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void Back() {
        middlePanel.SetActive(true);
        instructionsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
