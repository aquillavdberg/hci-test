using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodySourceView;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ButtonPressing : MonoBehaviour
{
    // public GameObject Button;
    SpriteRenderer sprite;
    public float waitTime;
    // public bool AllJointsCollided
    WaitForSecondsRealtime waitForSecondsRealtime;
    public float LateralStretchTimer;
    // public GameObject LateralStretch2;
    public int doneStretch = 0;
    public int doneMirror = 0;

// ToDo's:

// render skeleton in front of the stretch
// add color to sprite (@stretching musle)
// add body parts button @stretch exercise scene
// add "next stretch" button @completion current exercise

// maak dictionary van alle joint colliders om if all colliders collided -> "hold that pose!!"

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "LateralStretch")
        {
            LateralStretchTimer = Time.time;
            // Debug.Log("timer is set at" + LateralStretchTimer);d
        }
    }

    // Update is called once per frame
    public void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "LateralStretch")
        {
            var TimeDif = Time.time - LateralStretchTimer;
            // Debug.Log("time is at" + Time.time + "the difference is" + TimeDif);

            if (Time.time - LateralStretchTimer >= 25)
            {
                
                if (doneMirror == 0)
                {
                    GameObject LateralStretch = GameObject.FindWithTag("StretchContainer");
                    LateralStretch.transform.GetChild(0).gameObject.SetActive(false);
                    // GameObject LateralStretchFlip = GameObject.FindWithTag("LateralStretchFlip");
                    LateralStretch.transform.GetChild(1).gameObject.SetActive(true);
                    // LateralStretch.transform.localScale = new Vector3(-3, 3, 2);
                    doneMirror += 1;
                }
            }

            if (Time.time - LateralStretchTimer >= 40)
            {
                // Debug.Log("Time.time - LateralStretchTimer >= 35");
                if (doneStretch == 0)
                {
                    // Debug.Log("doneStretch = " + doneStretch);
                    GameObject doneStretching = GameObject.FindWithTag("GoodJob");
                    // Debug.Log(doneStretching.name);
                    doneStretching.transform.GetChild(0).gameObject.SetActive(true);
                    // add "next stretch" button @completion current exercise
                    
                    doneStretch += 1;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        // Debug.Log("wel getriggered = " + this.gameObject.name);
        
        
        if (this.gameObject.name == "No") 
        {
            Debug.Log("no");
            Application.Quit();
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "LateralStretch" && scene.name != "LateralStretch2")
        {
            SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
            System.Threading.Thread.Sleep(1000);
        }
        else
        {
            if (this.gameObject.name == other.gameObject.name)
            {
            sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.color = new Color (0, 255, 0, 255);
            sprite = other.gameObject.GetComponent<SpriteRenderer>();
            sprite.color = new Color (0, 1, 0, 1); 
            // AllJointsCollided = true
            // BodySourceView.jointCollided[other.gameObject.name] = true;
            // foreach in jointcollided[]
            //      if jointcollided[i] == true {do nothing}
            //      if jointcollided[i] == false {AllJointsCollided = false}
            // if AllJointsCOllided == true {pop up "hold that pose" of add audio fragment o.i.d.}
            }

            if (this.gameObject.name == "Torso") 
            {
                SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
                System.Threading.Thread.Sleep(1000);
            }

            if (this.gameObject.name == "LateralStretch2")
            {
                SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
                System.Threading.Thread.Sleep(1000);
            }
        }
        

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
            if (this.gameObject.name == collision.gameObject.name)
            {
                // BodySourceView.jointCollided[collision.gameObject.name] = false;
                sprite = gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (255, 0, 255, 255);
                sprite = collision.gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (1, 0, 0, 1); 
            }

    }
    
}