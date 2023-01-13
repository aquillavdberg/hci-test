using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipStretch : MonoBehaviour
{
    public float stretchTime = 30f;
    public float spriteScale = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("FlipStretchSprite", stretchTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FlipStretchSprite()
    {
        float xFlip = spriteScale * -2;
        Vector3 scaleFlip = new Vector3(xFlip, 0);
        transform.localScale += scaleFlip;
    }
}
