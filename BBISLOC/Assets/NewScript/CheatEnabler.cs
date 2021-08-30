using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using Unity;
using UnityEditor;

public class CheatEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(EnablerKey))
        {
            this.CheatsMenu.SetActive(true);
            this.gc.UnlockMouse();
            this.gc.PauseGameLight();
        }
    }
    public KeyCode EnablerKey;
    public GameObject CheatsMenu;
    public GameControllerScript gc;
}
