using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
// using static EditorWindow;

public class EditorWindowWithPopup : MonoBehaviour
{

    public static GameObject popupObj;
    // popupObj.ge

    // Add menu item
    // [MenuItem("Confirm navigation")]
    public static void Init()
    {
        GameObject popupObj = new GameObject("popupObj");
        popupObj.AddComponent()
    }

   public static Rect buttonRect;
    public static string OnGUI(GameObject gameObject)
    {
        {
            if (gameObject.name == "No")
                {GUILayout.Label("Are you sure you want to exit the application?", EditorStyles.boldLabel);}
            else
                {GUILayout.Label("Are you sure you want to go to" + gameObject.name + "?", EditorStyles.boldLabel);}
            
            if (GUILayout.Button("Yes", GUILayout.Width(200)))
                {return "true";}
            if(GUILayout.Button("No", GUILayout.Width(200)))
                {return "false";}

            if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect();

            return "still waiting";
        }
    }


    public static void Remove()
    {
        Destroy(popupObj);
    }
}

