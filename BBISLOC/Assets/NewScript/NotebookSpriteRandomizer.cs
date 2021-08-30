using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookSpriteRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int RandomNumberForArray = UnityEngine.Random.Range(0, sprites.Length);
        spriteRen.sprite = sprites[RandomNumberForArray];
    }
    public SpriteRenderer spriteRen;
    public Sprite[] sprites = new Sprite[1];
}
