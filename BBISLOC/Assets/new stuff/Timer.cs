using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        base.StartCoroutine("Timers");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Timers()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        this.time--;
        if (this.time < 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            this.timeText.text = "Time: " + this.time;
            base.StartCoroutine("Timers");
        }
        yield break;
    }

    public Text timeText;

    public GameObject timer;

    public GameObject timecontroller;

    public int time;
}
