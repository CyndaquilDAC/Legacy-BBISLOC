using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        this.ec.CurrentButton = this.ButtonSetting;
    }
    public string ButtonSetting;
    public EditorController ec;
}
