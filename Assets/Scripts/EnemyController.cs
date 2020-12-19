using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public bool blueEnemy, greenEnemy, purpleEnemy, redEnemy;
    public float speed;

    [SerializeField]
    private float deathSeconds; //time until despawn when health hits 0


    [SerializeField]
    GameObject target;

    Rigidbody2D body;
    HealthController health;

    public Animator anim;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<HealthController>();
        anim = GetComponent<Animator>();


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
        if (health <= (maxHealth-(maxHealth * .50))) //50% health
        {
            anim.SetBool("Hurt", true);
        }
        else if (health <= 0) {
            StartCoroutine(Despawn());
        }
    }

    public IEnumerator Despawn() // We don't want the enemy to despawn or "disable" immediately
    {
        WaveSpawner.aliveEnemies--;
        speed = 0;
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(deathSeconds);
        gameObject.SetActive(false);
    }

}
