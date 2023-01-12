using UnityEngine;
using UnityEditor;

public class EditorWindowWithPopup : EditorWindow
{
    // Add menu item
    [MenuItem("Confirm navigation")]
    static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<EditorWindowWithPopup>();
        window.Show();
    }

    Rect buttonRect;
    bool OnGUI(GameObject gameObject)
    {
        {
            if (gameObject.name == "No")
                {GUILayout.Label("Are you sure you want to exit the application?", EditorStyles.boldLabel);}
            else
                {GUILayout.Label("Are you sure you want to go to" + gameObject.name + "?", EditorStyles.boldLabel);}
            
            if (GUILayout.Button("Yes", GUILayout.Width(200)))
                {return true;}
            if(GUILayout.Button("No", GUILayout.Width(200)))
                {return false;}

            if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect();
        }
    }


    void Remove()
    {
        window.Destroy;
    }
}

