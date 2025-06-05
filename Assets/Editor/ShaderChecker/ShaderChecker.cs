using UnityEditor;
using UnityEngine;

public class ShaderChecker
{
    [MenuItem("Tools/Check For Missing Shaders")]
    public static void CheckForMissingShaders()
    {
        var materialGuids = AssetDatabase.FindAssets("t:Material");
        foreach (var guid in materialGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat != null && (mat.shader == null || mat.shader.name == "Hidden/InternalErrorShader"))
            {
                Debug.LogWarning($"Material '{mat.name}' at '{path}' is missing a valid shader!");
            }
        }
    }
}