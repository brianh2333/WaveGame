using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 250;
    public HealthSystem healthSystem;
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealth(health);
    }

}
