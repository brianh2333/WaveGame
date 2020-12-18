using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;

    public event EventHandler<OnDamagedEventArgs> OnDamaged;

    public class OnDamagedEventArgs : EventArgs
    {
        public float amount;
    }


    public void SetHealth(float mHealth)
    {
        maxHealth = mHealth;
        currentHealth = maxHealth;
    }

    public void NormalizeHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float health)
    {
        OnDamaged?.Invoke(this, new OnDamagedEventArgs { amount = health });
        currentHealth -= health;
    }

    public void Heal(float health)
    {
        currentHealth += health;
        if(currentHealth > maxHealth)
        {
            NormalizeHealth();
        }
    }


}
