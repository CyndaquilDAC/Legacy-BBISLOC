using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChallengesButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("ChallengesUnlocked"))
        {
            ChallengesButton.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("ChallengesUnlocked"))
        {
            ChallengesButton.SetActive(true);
        }
    }

    public GameObject ChallengesButton;
}
