using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;

    void Update() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D c) {
        HitObject(c.gameObject);
        this.gameObject.SetActive(false);
        GameManager.instance.AddCurrency(c.name);
    }

    void HitObject(GameObject g) {
        HealthController health = g.GetComponent<HealthController>();
        if (health != null) {
            health.TakeDamage(FireProjectile.instance.attackDamage);
        }
    }
}
