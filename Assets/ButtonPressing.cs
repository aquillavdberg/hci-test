using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressing : MonoBehaviour
{
    public float waitTime;
    WaitForSecondsRealtime waitForSecondsRealtime;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      
        Destroy(this.gameObject);
    }

   
   
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        Destroy(this.gameObject);
    }
}
