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
    
    void Awake() {
        health = maxHealth; //Health starts at Max health
        if (isPlayer) playerHealthSlider.value = health;
    }

    public void TakeDamage(float damage) {
        canChange = true;
        float oldHealth = health;
        if (isPlayer) playerHealthSlider.value = health;
        
        if (canChange) {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            onHealthChanged(oldHealth, health);
        }

        health -= damage;
        
        if (isPlayer && health <= 0) {
            SceneManager.LoadScene(0);
        }
    }
}
