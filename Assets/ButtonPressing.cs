using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodySourceView;
using UnityEngine.SceneManagement;
using UnityEditor;
using static EditorWindowWithPopup;


// ToDo's:

// on button clicks: pop up "please confirm" met gestures? duim omhoog/duim omlaag?
// add home button
// render skeleton in front of the stretch
// add color to sprite @stretching musle
// add audio support:
//  "take a moment to breath here" 
// "in through your nose, out through your mouth" 
// "feel free to slowly lean more and more into the stretch"

// maak dictionary van alle joint colliders om if all colliders collided -> "hold that pose!!"

public class ButtonPressing : MonoBehaviour
{
    SpriteRenderer sprite;
    public float waitTime;
    // public bool AllJointsCollided
    WaitForSecondsRealtime waitForSecondsRealtime;

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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "LateralStretch")
        {
            if (this.gameObject.name == other.gameObject.name)
            {
            sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.color = new Color (0, 1, 0, 1); 
            sprite = other.gameObject.GetComponent<SpriteRenderer>();
            sprite.color = new Color (0, 1, 0, 1); 
            // AllJointsCollided = true
            // BodySourceView.jointCollided[currentJoint] = true;
            // foreach in jointcollided[]
            //      if jointcollided[i] == true {do nothing}
            //      if jointcollided[i] == false {AllJointsCollided = false}
            // if AllJointsCOllided == true {pop up "hold that pose" of add audio fragment o.i.d.}
            }
            else{
                // do hier popup unit
                EditorWindowWithPopup.Init();
                if (this.gameObject.name == "Torso") 
                {
                    while (EditorWindowWithPopup.OnGUI(gameObject)== "still waiting"){}
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "true") 
                    {
                        SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
                    }
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "false") 
                    {
                        EditorWindowWithPopup.Remove();
                    }
                }
                if (this.gameObject.name == "No") 
                {
                    while (EditorWindowWithPopup.OnGUI(gameObject)== "still waiting"){}
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "true")
                        {Debug.Log("no");
                        Application.Quit();}
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "false") 
                    {
                        EditorWindowWithPopup.Remove();
                    }
                }
            }

        }
        else {
            EditorWindowWithPopup.Init();
            if (this.gameObject.name == "No") 
                {
                    while (EditorWindowWithPopup.OnGUI(gameObject)== "still waiting"){}
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "true")
                        {Debug.Log("no");
                        Application.Quit();}
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "false") 
                    {
                        EditorWindowWithPopup.Remove();
                    }
                }
            else
            {
                while (EditorWindowWithPopup.OnGUI(gameObject)== "still waiting"){}
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "true") 
                    {
                        SceneManager.LoadScene(this.gameObject.name, LoadSceneMode.Single);
                    }
                    if (EditorWindowWithPopup.OnGUI(gameObject)== "false") 
                    {
                        EditorWindowWithPopup.Remove();
                    }
            }
        } 
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
            if (this.gameObject.name == collision.gameObject.name)
            {
                // BodySourceView.jointCollided[currentJoint] = false;
                sprite = gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (1, 0, 0, 1);
                sprite = collision.gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color (1, 0, 0, 1); 
            }

    }
    
}