using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour {

    public static FireProjectile instance;

    public Rigidbody2D bullet;
    public float fireRate = 5f;
    public float attackDamage = 10f;
    float nextTimeToFire = 0f;
    public Slider fireRateSlider;
    public Slider attackDamageSlider;
    public AudioSource turretShootSound;

    void Awake() {
        if (instance==null) instance = this;
    }

    void Update() {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !GameManager.instance.shopMenuOpen) {
            nextTimeToFire = Time.time + 3.2f/fireRate;
            Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            turretShootSound.Play();

        }
        fireRateSlider.value = fireRate;
        fireRateSlider.GetComponentInChildren<Text>().text = fireRate.ToString("F2"); //Round to 2 decimals.
        attackDamageSlider.value = attackDamage;
        attackDamageSlider.GetComponentInChildren<Text>().text = attackDamage.ToString("F2");
    }

    public void FireRateIncrease() {        
        if (GameManager.instance.currency >= 5) {
            fireRate += (fireRate * 0.10f);
            GameManager.instance.currency -= 5f;
        }
    }

    public void AttackDamageIncrease() {
        if (GameManager.instance.currency >= 10) {
            attackDamage += (attackDamage * 0.20f);
            GameManager.instance.currency -= 10f;
        }
    }
}
