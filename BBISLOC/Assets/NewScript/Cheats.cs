using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void BaldiEnabledToggleVoid()
    {
        if (BaldiObject.activeSelf == true);
        {
            BaldiObject.SetActive(false);
        }
        if (BaldiObject.activeSelf == false);
        {
            BaldiObject.SetActive(true);
        }
    }

    public TMP_InputField BaldiSpeedField;
    public Toggle BaldiEnabledToggle;
    public GameObject BaldiObject;
    public float BaldiSpeedFloat;
    public BaldiScript BaldiScript;
    public NavMeshAgent BaldiAgent;

    // Update is called once per frame
    void Update()
    {
        BaldiAgent.angularSpeed = BaldiSpeedFloat;
        BaldiAgent.speed = BaldiSpeedFloat;
    }
}
