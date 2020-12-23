using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public bool blueEnemy, greenEnemy, purpleEnemy, redEnemy;
    public float speed;
    public float attackDamage = 50;

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
        if (health <= 0) {
            StartCoroutine(Despawn());
        }

        else if (health <= (maxHealth-(maxHealth * .50))) //50% health
        {
            anim.SetBool("Hurt", true);
            Debug.Log("Enemy hurt");
        }
    }

    public IEnumerator Despawn() {
        Debug.Log("Despawn coroutine called");
        WaveSpawner.aliveEnemies--;
        speed = 0;
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(deathSeconds);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.CompareTag("Player")) {
            HitObject(c.gameObject);
            StartCoroutine(Despawn());
        }
    }

    void HitObject(GameObject g) {
        HealthController health = g.GetComponent<HealthController>();
        if (health != null) {
            health.TakeDamage(attackDamage);
        }
    }
}
