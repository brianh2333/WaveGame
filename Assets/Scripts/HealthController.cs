using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {
    
    public delegate void OnHealthChanged (float previousHealth, float health);
	public event OnHealthChanged onHealthChanged = delegate {};

    public float maxHealth = 100;
    public float health;

    public bool isEnemy, isPlayer;
    bool canChange;
    public Slider playerHealthSlider;
    public Text healthText;
    
    void Awake() {
        health = maxHealth; //Health starts at Max health
        if (isPlayer)
        {
            playerHealthSlider.value = health;
            healthText = playerHealthSlider.GetComponentInChildren<Text>();
            healthText.text = playerHealthSlider.value.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void TakeDamage(float damage) {
        canChange = true;
        float oldHealth = health;
        
        if (canChange) {
            health -= damage;
            if (isPlayer)
            {
                playerHealthSlider.value = health;
                healthText.text = playerHealthSlider.value.ToString() + " / " + maxHealth.ToString();
            }
            health = Mathf.Clamp(health, 0, maxHealth);
            if (isEnemy)
                onHealthChanged(oldHealth, health);
        }
        
        if (isPlayer && health <= 0) {
            SceneManager.LoadScene(0);
        }
    }
}
