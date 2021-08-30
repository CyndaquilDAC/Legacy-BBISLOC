using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFreeRunModeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("FreeRunModeUnlocked"))
        {
            FreeRunButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("FreeRunModeUnlocked"))
        {
            FreeRunButton.SetActive(true);
        }
    }
    public GameObject FreeRunButton;
}
