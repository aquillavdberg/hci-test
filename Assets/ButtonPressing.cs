using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodySourceView;

public class ButtonPressing : MonoBehaviour
{
    public float waitTime;
    WaitForSecondsRealtime waitForSecondsRealtime;

// maak dictionary van alle joint colliders

    // Start is called before the first frame update
    void Start()
    {
        // if this.gameobject == "jointcollider": gebruik basic sprite
        // misschien zelfs niet nodig, gebruik prefab voor de jointcolliders?
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      
        // Destroy(this.gameObject);
        Debug.Log("Enter");
        Debug.Log(gameObject);

        Debug.Log("BodySourceView.jointObj");
        foreach (GameObject i in BodySourceView.joints)
            {
            Debug.Log(i);
            }
        

        
        // if (this.gameobject == "yes") {next scene}
        // if (this.gameobject == "no") {previous scene o.i.d.}
    }

   
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("Stay");
        // Debug.Log(this.gameObject);
        // Debug.Log(gameObject);

        // if this.gameobject == "jointcollider" && tag == correcte joint: 
            // {gebruik fancy sprite
            // @ dict this.gameobject = true
                // if @dict all gameobjects == true add text "hold that pose"
            // }

    }

    // private void OnTriggerLeave2D(Collider2D collision) @ dict this.gameobject = false

}

// meer collissionboxen + tag checker of de correcte joint colide
