using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{
    string myString = "Default";

    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    } 

    void OnGUI()
    {
        GUILayout.Label("This is a label", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Description", myString);

        if (GUILayout.Button("Press Me"))
        {
            Debug.Log("Pressed");
        }
    }

    
}
