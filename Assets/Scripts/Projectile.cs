using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public int attackDamage = 10;

    void Update() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D c) {
        HitObject(c.gameObject);
        this.gameObject.SetActive(false);
        Debug.Log("Touched");
    }

    void HitObject(GameObject g) {
        HealthController health = g.GetComponent<HealthController>();
        if (health != null) {
            health.TakeDamage(attackDamage);
        }
    }
}
