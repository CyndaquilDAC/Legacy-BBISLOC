using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBeenWonCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HasBeenWon"));
        {
            this.CheatEnabler.SetActive(true);
        }
    }
    public GameObject CheatEnabler;
    // Update is called once per frame
    void Update()
    {
        
    }
}
