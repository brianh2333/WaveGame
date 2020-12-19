using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {

    public Rigidbody2D bullet;
    public float velocity;

    void Update() {
        if(Input.GetButtonDown("Fire1")) {
            Rigidbody2D newBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            newBullet.AddForce(transform.forward * velocity, ForceMode2D.Impulse);
        }
    }
}
