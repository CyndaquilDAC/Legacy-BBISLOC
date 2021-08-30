using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnEnter : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.tag == "Player")
        {
        Notebook.SetActive(true);
        AudioSource.PlayOneShot(BuzzSound);
        }
    }
    public AudioClip BuzzSound;
    public GameObject Notebook;
    public AudioSource AudioSource;
}