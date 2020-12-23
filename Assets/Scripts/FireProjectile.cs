using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour {

    public Rigidbody2D bullet;
    public float fireRate = 5f;
    float nextTimeToFire = 0f;
    public Slider fireRateSlider;
    public AudioSource turretShootSound;

    void Update() {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !GameManager.instance.shopMenuOpen) {
            nextTimeToFire = Time.time + 3.2f/fireRate;
            Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            turretShootSound.Play();

        }
        fireRateSlider.value = fireRate;
        fireRateSlider.GetComponentInChildren<Text>().text = fireRate.ToString("F2"); //Round to 2 decimals.
    }

    public void FireRateIncrease() {        
        if (GameManager.instance.currency >= 5) {
            fireRate += (fireRate * 0.10f);
            GameManager.instance.currency -= 5f;
        }
    }
}
