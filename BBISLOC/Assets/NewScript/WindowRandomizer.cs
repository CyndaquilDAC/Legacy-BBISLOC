using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int RandomNumberForArray = UnityEngine.Random.Range(0, Materials.Length);
        MeshRen1.material = Materials[RandomNumberForArray];
        MeshRen2.material = Materials[RandomNumberForArray];
    }
    public MeshRenderer MeshRen1;
    public MeshRenderer MeshRen2;
    public Material[] Materials = new Material[1];
}
