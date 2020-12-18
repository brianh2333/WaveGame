using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float dmg;

    public GameManager gameManager;


    public class OnImpactEventArgs : EventArgs
    {
        public string name;
    }

    private void OnEnable()
    {
        Invoke(nameof(DestroyProjectile), lifetime);
    }

    private void Awake()
    { 
         gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameManager.AddCurrency(collision.name);
            DestroyProjectile();
            collision.GetComponent<HealthSystem>().TakeDamage(dmg);
        }
    }


    void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
