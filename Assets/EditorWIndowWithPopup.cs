using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
// using static EditorWindow;

public class EditorWindowWithPopup : EditorWindow
{
    public static EditorWindow window;
    // Add menu item
    // [MenuItem("Confirm navigation")]
    public static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<EditorWindow>();
        window.Show();
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
        // Destroy(window);
        UnityEngine.Object.Destroy(window);
    }
}

