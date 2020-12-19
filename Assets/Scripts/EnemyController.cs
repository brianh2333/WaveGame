using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public bool blueEnemy, greenEnemy, purpleEnemy;
    public float speed;

    [SerializeField]
    GameObject target;

    Rigidbody2D body;
    HealthController health;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<HealthController>();
    }

    void OnEnable() {
		health.onHealthChanged += HealthChanged;
	}

	void OnDisable() {
		health.onHealthChanged -= HealthChanged;
	}

    void Update() {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        body.velocity = dir * speed;
    }

    public void HealthChanged(float maxHealth, float health) {
        if (health <= 0) {
            this.gameObject.SetActive(false);
            Debug.Log("Enemy Died");
        }
    }
}
