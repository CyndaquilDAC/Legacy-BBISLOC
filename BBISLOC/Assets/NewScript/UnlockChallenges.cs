using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockChallenges : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPress()
    {
        if (ShopPointsFloat == 500)
        {
            PlayerPrefs.SetFloat("ChallengesUnlocked", 1f);
            PlayerPrefs.SetFloat("ShopPoints", PlayerPrefs.GetFloat("ShopPoints") - 500f);
            //set unlocked
        }
        if (ShopPointsFloat <= 500)
        {
            //Do nothing.
        }
        if (ShopPointsFloat >= 500)
        {
            PlayerPrefs.SetFloat("ChallengesUnlocked", 1f);
            PlayerPrefs.SetFloat("ShopPoints", PlayerPrefs.GetFloat("ShopPoints") - 500f);
            //Set unlocked
        }
    }

    public GameObject ChallengesUnlockButton;
    public string PointsNumberString;
    public float ShopPointsFloat;
    // Update is called once per frame
    void Update()
    {
        ShopPointsFloat = PlayerPrefs.GetFloat("ShopPoints");
        if (PlayerPrefs.HasKey("ChallengesUnlocked"))
        {
            ChallengesUnlockButton.SetActive(false);
        }
    }
}
