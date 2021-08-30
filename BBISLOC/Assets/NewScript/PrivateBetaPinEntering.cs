using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrivateBetaPinEntering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public TMP_InputField InputField;
    public string CorrectPin;

    public void OnContinueButtonPress()
    {
        if (InputField.text == CorrectPin)
        {
            SceneManager.LoadSceneAsync("Warning");
        }
        else if (InputField.text != CorrectPin)
        {
            SceneManager.LoadSceneAsync("PrivatePinError");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
