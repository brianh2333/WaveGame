using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireProjectiles : MonoBehaviour
{
    [SerializeField]
    private Transform prefBullet;

    public Transform shotPosition;

    public ObjectPooler objPooler;

    public float fireRate;
    private float fireRateSeconds = 0;

    // Update is called once per frame
    void Update()
    {
        if (fireRateSeconds <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                objPooler.SpawnFromPool("PongBall", shotPosition.position, shotPosition.rotation);
                fireRateSeconds = fireRate;
            }
        }
        else
            fireRateSeconds -= Time.deltaTime;
    }
}
