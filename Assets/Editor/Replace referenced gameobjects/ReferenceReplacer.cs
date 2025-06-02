using UnityEngine;
using UnityEditor;

public class ReferenceReplacer : EditorWindow
{
    Object oldReference;
    Object newReference;

    [MenuItem("Tools/Replace GameObject References")]
    public static void ShowWindow()
    {
        GetWindow<ReferenceReplacer>("Replace References");
    }

    void OnGUI()
    {
        oldReference = EditorGUILayout.ObjectField("Old Reference", oldReference, typeof(Object), true);
        newReference = EditorGUILayout.ObjectField("New Reference", newReference, typeof(Object), true);

        if (GUILayout.Button("Replace References"))
        {
            ReplaceAllReferences();
        }
    }

    void ReplaceAllReferences()
    {
        if (oldReference == null || newReference == null)
        {
            Debug.LogWarning("Please assign both old and new references.");
            return;
        }

        var allObjects = FindObjectsByType<Component>(FindObjectsSortMode.None);
        int replacedCount = 0;

        foreach (var obj in allObjects)
        {
            SerializedObject so = new SerializedObject(obj);
            SerializedProperty prop = so.GetIterator();

            while (prop.NextVisible(true))
            {
                if (prop.propertyType == SerializedPropertyType.ObjectReference &&
                    prop.objectReferenceValue == oldReference)
                {
                    prop.objectReferenceValue = newReference;
                    replacedCount++;
                }
            }

            so.ApplyModifiedProperties();
        }

        Debug.Log($"Replaced {replacedCount} references in scene objects.");
    }
}
