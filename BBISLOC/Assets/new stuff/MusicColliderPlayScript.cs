using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicColliderPlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.outsidemusic.SetActive(true);
        this.schoolmusic.SetActive(false);
        this.brickcollider.SetActive(true);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.schoolmusic.SetActive(true);
            this.outsidemusic.SetActive(false);
            this.brickcollider.SetActive(false);
        }
    }

    public GameObject schoolmusic;

    public GameObject outsidemusic;

    public GameObject brickcollider;
}
