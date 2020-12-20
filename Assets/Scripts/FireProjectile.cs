using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour {

    public Rigidbody2D bullet;
    public float velocity;
    public float fireRate = 5f;
    float nextTimeToFire = 0f;
    public Slider fireRateSlider;
    public AudioSource turretShootSound;

    void Update() {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 3.2f/fireRate;
            Rigidbody2D newBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            newBullet.AddForce(transform.forward * velocity, ForceMode2D.Impulse);
            turretShootSound.Play();
        }
        fireRateSlider.value = fireRate;
    }

    public void FireRateIncrease() {        
        if (GameManager.instance.currency >= 5) {
            fireRate *= 2.25f;
            GameManager.instance.currency = 0f;
        }
    }
}
