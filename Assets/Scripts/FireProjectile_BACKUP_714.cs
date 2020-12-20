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
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 3.2f/fireRate;
<<<<<<< HEAD
            Instantiate(bullet, transform.position + transform.forward, transform.rotation);
=======
            Rigidbody2D newBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            newBullet.AddForce(transform.forward * velocity, ForceMode2D.Impulse);
            turretShootSound.Play();
>>>>>>> 4978fb0340271feeb54346b545e8ab91c94e735d
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
