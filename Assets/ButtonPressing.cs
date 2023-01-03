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
        Debug.Log(other.gameObject.name);
        //Destroy(this.gameObject);
    }

    IEnumerator DoWait()
    {
        Debug.Log("Start wait");
        if (waitForSecondsRealtime == null)
            waitForSecondsRealtime = new WaitForSecondsRealtime(waitTime);
        else
            waitForSecondsRealtime.waitTime = waitTime;
        yield return waitForSecondsRealtime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Staying");
        StartCoroutine("DoWait");
        Destroy(this.gameObject);
    }
}
