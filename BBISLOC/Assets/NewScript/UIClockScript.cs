using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIClockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.clock.text = string.Concat(new object[]
        {
            DateTime.Now.Hour,
            ":",
            DateTime.Now.Minute,
            ":",
            DateTime.Now.Second
        });
    }
    public TextMeshPro clock;
}
