using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    
    public delegate void OnHealthChanged (float previousHealth, float health);
	public event OnHealthChanged onHealthChanged = delegate {};

    public float maxHealth = 100;
    public float health;

    public bool blueEnemy, greenEnemy, purpleEnemy, Player;
    bool canChange;
    bool isDead = false;
    
    void Awake() {
        health = maxHealth; //Health starts at Max health
    }

    public void TakeDamage(float damage) {
        canChange = true;
        float oldHealth = health;

        if (canChange) {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            onHealthChanged(oldHealth, health);
        }

        health -= damage;
        if (health <= 0) {
            isDead = true;
        }
    }
}
