using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCountdown : MonoBehaviour
{
    public WaveSpawner spawner;
    public ModifyTimer timer;
    public GlobalTimer globalTimer;
    public TextMeshProUGUI countdownText;
    public GameObject countdown;
    public Animator countdownAnim;
    private float counttime = 0;

    private void Start()
    {
        countdownText = countdown.GetComponentInChildren<TextMeshProUGUI>();
        countdownAnim = countdown.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (globalTimer.nextWave > 0 && spawner.status == WaveSpawner.WaveStatus.COUNTING)
        {
            counttime = globalTimer.nextWave;
            countdown.SetActive(true);
            countdownAnim.SetTrigger("FadeIn");
            countdownText.text = "Next wave: \n" + counttime.ToString("F2");
        }
        else
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        countdownAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        countdown.SetActive(false);
    }
}
