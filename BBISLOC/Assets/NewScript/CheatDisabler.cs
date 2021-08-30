using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatDisabler : MonoBehaviour
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
            this.CheatsMenu.SetActive(false);
            this.gc.LockMouse();
            this.gc.UnpauseGameLight();
        }
    }
    public KeyCode EnablerKey;
    public GameObject CheatsMenu;
    public GameControllerScript gc;
}
