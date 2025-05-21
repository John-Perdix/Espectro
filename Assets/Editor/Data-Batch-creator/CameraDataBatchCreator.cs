// Assets/Editor/CameraDataBatchCreator.cs

using UnityEditor;
using UnityEngine;
using Unity.Cinemachine;
using System.IO;

public class CameraDataBatchCreator : EditorWindow
{
    private string savePath = "Assets/_Scripts/Cameras/CameraData";

    [MenuItem("Tools/Camera/Batch Create CameraData")]
    public static void ShowWindow()
    {
        GetWindow<CameraDataBatchCreator>("Batch CameraData Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Create CameraData Assets", EditorStyles.boldLabel);

        GUILayout.Space(5);
        GUILayout.Label("Save Folder:");
        savePath = EditorGUILayout.TextField(savePath);

        if (GUILayout.Button("Create CameraData for All Cinemachine Cameras in Scene"))
        {
            CreateCameraDataAssets();
        }
    }

    private void CreateCameraDataAssets()
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            AssetDatabase.Refresh();
        }

        string prefabFolder = "Assets/_Scripts/Cameras/CameraData/Prefabs";
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabFolder });

        int created = 0;

        foreach (string guid in prefabGuids)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogWarning($"Could not load prefab at path: {prefabPath}");
                continue;
            }

            CinemachineCamera cam = prefab.GetComponentInChildren<CinemachineCamera>();
            if (cam == null)
            {
                Debug.LogWarning($"No CinemachineCamera found in prefab: {prefab.name}");
                continue;
            }

            string assetName = $"{prefab.name}_CameraData";
            string assetPath = Path.Combine(savePath, assetName + ".asset");

            if (File.Exists(assetPath))
            {
                Debug.LogWarning($"Skipped existing: {assetPath}");
                continue;
            }

            CameraData data = ScriptableObject.CreateInstance<CameraData>();
            data.camera = cam;
            data.label = prefab.name.Replace("Camera", "").Trim();

            AssetDatabase.CreateAsset(data, assetPath);
            created++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ Created {created} CameraData assets from prefabs in: {prefabFolder}");
    }

}
