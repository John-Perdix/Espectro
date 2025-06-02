using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class CinemachineSwitcherDynamic : MonoBehaviour
{
    public CameraData[] cameraDataArray;
    public CinemachineVirtualCameraBase[] cameraInstances;
    public int activeCameraIndex = 1;

    [Header("UI Elements of the active camera")]
    public Image cameraImage;
    public TextMeshProUGUI cameraLabel;
    

    [Header("Button Previews for the previous and next cameras")]
    public Image nextCameraImage;
    public TextMeshProUGUI nextCameraLabel;
    public Image previousCameraImage;
    public TextMeshProUGUI previousCameraLabel;


    private void Awake()
{
#if UNITY_EDITOR
    if (!Application.isPlaying) return;
#endif
    cameraInstances = new CinemachineVirtualCameraBase[cameraDataArray.Length];
    for (int i = 0; i < cameraDataArray.Length; i++)
    {
        var prefab = cameraDataArray[i].camera;
        if (prefab != null && prefab != gameObject)
        {
            var instance = Instantiate(prefab, transform);
            instance.transform.localPosition = prefab.transform.localPosition;
            instance.transform.localRotation = prefab.transform.localRotation;
            cameraInstances[i] = instance; // Store instance locally, not in the asset!
        }
    }

    ActivateCamera(activeCameraIndex);
    UpdateUI();
}

    public void ActivateCamera(int index)
    {
        if (index < 0 || index >= cameraDataArray.Length)
        {
            Debug.LogWarning("Invalid camera index or index 0 is disabled");
            return;
        }

        for (int i = 0; i < cameraInstances.Length; i++)
        {
            if (cameraInstances[i] != null)
                cameraInstances[i].Priority = (i == index) ? 10 : 5;
        }

        activeCameraIndex = index;
        UpdateUI();
    }

    public void IncrementCameraIndex()
    {
        int nextIndex = (activeCameraIndex + 1) % cameraDataArray.Length;
        ActivateCamera(nextIndex);
    }

    public void DecrementCameraIndex()
    {
        int prevIndex = (activeCameraIndex - 1 + cameraDataArray.Length) % cameraDataArray.Length;
        ActivateCamera(prevIndex);
    }

    public void UpdateUI()
    {
        if (cameraImage != null)
            cameraImage.sprite = cameraDataArray[activeCameraIndex].image;

        if (cameraLabel != null)
            cameraLabel.text = cameraDataArray[activeCameraIndex].label;

        // Next preview
        int nextIndex = (activeCameraIndex + 1) % cameraDataArray.Length;
        while (cameraDataArray[nextIndex].camera == null && nextIndex != activeCameraIndex)
            nextIndex = (nextIndex + 1) % cameraDataArray.Length;

        if (nextCameraImage != null)
            nextCameraImage.sprite = cameraDataArray[nextIndex].image;

        if (nextCameraLabel != null)
            nextCameraLabel.text = cameraDataArray[nextIndex].label;

        // Previous preview
        int prevIndex = (activeCameraIndex - 1 + cameraDataArray.Length) % cameraDataArray.Length;
        while (cameraDataArray[prevIndex].camera == null && prevIndex != activeCameraIndex)
            prevIndex = (prevIndex - 1 + cameraDataArray.Length) % cameraDataArray.Length;

        if (previousCameraImage != null)
            previousCameraImage.sprite = cameraDataArray[prevIndex].image;

        if (previousCameraLabel != null)
            previousCameraLabel.text = cameraDataArray[prevIndex].label;
    }

}
