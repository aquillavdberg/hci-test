using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodySourceView;
using UnityEngine.SceneManagement;

public class ButtonPressing : MonoBehaviour
{
    SpriteRenderer sprite;
    public float waitTime;
    WaitForSecondsRealtime waitForSecondsRealtime;

// maak dictionary van alle joint colliders om if all colliders collided -> "hold that pose!!"

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (this.gameObject.name == "Back") 
        // {
        //     Debug.Log("Back");
        //     SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        // }

        // if (this.gameObject.name == "Yes") 
        // {
        //     Debug.Log("yes");
        //     SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
        // }
        
        Debug.Log("wel getriggered = " + this.gameObject.name);
        
        
        if (this.gameObject.name == "No") 
        {
            Debug.Log("no");
            Application.Quit();
            // {previous scene o.i.d. of pop-up "you want to exit the programm?"}
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Scene1")
        {
            SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);  
        }
        else
        {
            if (this.gameObject.name == other.gameObject.name)
            {
            sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.color = new Color (0, 1, 0, 1); 
            sprite = other.gameObject.GetComponent<SpriteRenderer>();
            sprite.color = new Color (0, 1, 0, 1); 
            }

            if (this.gameObject.name == "MainScene") 
        {
            SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
            // {previous scene o.i.d. of pop-up "you want to exit the programm?"}
        }
        }
        

    }

   
   
    private void OnTriggerStay2D(Collider2D collision)
    {
            if (this.gameObject.name == collision.gameObject.name)
            {
                sprite = gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (0, 1, 0, 1); 
                sprite = collision.gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (0, 1, 0, 1); 
            }
            // {gebruik fancy sprite
                // if @dict all gameobjects == true add text "hold that pose"
            // }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // foreach (GameObject currentJoint in BodySourceView.joints)
        // {
            if (this.gameObject.name == collision.gameObject.name)
            {
                // BodySourceView.jointCollided[currentJoint] = false;
                sprite = gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (1, 0, 0, 1);
                sprite = collision.gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (1, 0, 0, 1); 
            }
        // }
    }
    
}