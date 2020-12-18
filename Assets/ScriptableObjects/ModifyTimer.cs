using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTimer : MonoBehaviour
{
    public GlobalTimer globalTimer;
    public WaveSpawner spawner;
    public int timerVal;
    public int nextWaveVal;
    public bool globalCountdown;

    private bool canSpawn;

    private void Start()
    {
        globalTimer.nextWave = nextWaveVal;
        globalTimer.timer = timerVal;
        canSpawn = true;
    }

    private void Update() 
    {
        if (globalTimer.nextWave <= 0)                      //spawn next wave
        {
            if (( globalTimer.timer > 0f && spawner.status == WaveSpawner.WaveStatus.SPAWNING) || globalTimer.timer > 0f)
            {                                               
                if (canSpawn)
                {
                    spawner.status = WaveSpawner.WaveStatus.SPAWNING;
                    canSpawn = false;
                }
                globalTimer.timer -= Time.deltaTime;
            }                                              
            if (globalTimer.timer <= 0f || spawner.status == WaveSpawner.WaveStatus.COMPLETE)              
            {

                spawner.status = WaveSpawner.WaveStatus.COUNTING;
                globalTimer.timer = timerVal;
                globalTimer.nextWave = nextWaveVal;         
                globalCountdown = false;
            }
        }
        else if (globalTimer.nextWave > 0)         //do not spawn next wave
        {
            globalTimer.nextWave -= Time.deltaTime;  //decr countdown to next wave
            globalCountdown = true;   //countdown globally
            canSpawn = true;
        }
    }
}
