using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float dmg;

    public DisplayCurrency currency;


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
         currency = GameObject.Find("DisplayUI").GetComponent<DisplayCurrency>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DestroyProjectile();
            collision.GetComponent<HealthSystem>().TakeDamage(dmg);
            currency.AddCurrency(collision.name);
        }
    }

    void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
