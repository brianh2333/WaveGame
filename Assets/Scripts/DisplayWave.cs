using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWave : MonoBehaviour
{
    //public WaveSpawner spawner;
    public TextMeshProUGUI waveText;
    public GameObject waveCounter;
    // Start is called before the first frame update
    void Start()
    {
        //spawner = FindObjectOfType<GameObject>().GetComponent<WaveSpawner>();
        waveText = waveCounter.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = WaveSpawner.currentWave;
    }
}
