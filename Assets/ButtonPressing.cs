using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodySourceView;

public class ButtonPressing : MonoBehaviour
{
    SpriteRenderer sprite;
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
        // Debug.Log("Enter"+ this.gameObject.name);

        // foreach (GameObject i in BodySourceView.joints)
        //     {
        //     Debug.Log(i.name);
        //     }
        
        if (this.gameObject.name == "yes") 
        {
            Debug.Log("yes");
        }
        // {next scene}
        if (this.gameObject.name == "no") 
        {
            Debug.Log("no");
        }
        // {previous scene o.i.d.}

    }

   
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("Stay");

            if (this.gameObject.name == other.gameObject.name)
            {
                Debug.Log("joint en collider gematched");
                // BodySourceView.jointCollided[currentJoint] = true;
                sprite = gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (1, 0, 0, 1); 
            }
            // {gebruik fancy sprite
                // if @dict all gameobjects == true add text "hold that pose"
            // }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject currentJoint in BodySourceView.joints)
        {
            if (this.gameObject.name == currentJoint.name)
            {
                // BodySourceView.jointCollided[currentJoint] = false;
                sprite = gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (0, 1, 1, 0);
            }
        }
    }
    
}

// meer collissionboxen
