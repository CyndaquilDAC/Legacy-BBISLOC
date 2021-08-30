using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorBaldi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GC.ActivateSpoopMode();
            TutorBaldiAudioSource.PlayOneShot(ReadyOrNot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameControllerScript GC;
    public AudioSource TutorBaldiAudioSource;
    public AudioClip ReadyOrNot;
}
