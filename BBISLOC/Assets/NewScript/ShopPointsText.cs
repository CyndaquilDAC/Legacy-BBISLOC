using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPointsText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PointsString = System.Convert.ToString(PlayerPrefs.GetFloat("ShopPoints"));
        PointsText.text = "Shop Points: " + PointsString;
    }

    // Update is called once per frame
    void Update()
    {
        PointsString = System.Convert.ToString(PlayerPrefs.GetFloat("ShopPoints"));
        PointsText.text = "Shop Points: " + PointsString;
    }
    public TextMeshProUGUI PointsText;
    public string PointsString;
}
