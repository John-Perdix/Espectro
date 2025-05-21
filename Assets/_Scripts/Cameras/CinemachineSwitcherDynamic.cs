using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CinemachineSwitcherDynamic : MonoBehaviour
{
    public CameraData[] cameraDataArray;
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
    for (int i = 0; i < cameraDataArray.Length; i++)
{
    var prefab = cameraDataArray[i].camera;
    if (prefab != null && prefab != gameObject)
    {
        var instance = Instantiate(prefab, transform); // Parent set
        instance.transform.localPosition = prefab.transform.localPosition;
        instance.transform.localRotation = prefab.transform.localRotation;
        cameraDataArray[i].camera = instance;
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

        for (int i = 0; i < cameraDataArray.Length; i++)
        {
            if (cameraDataArray[i].camera != null)
                cameraDataArray[i].camera.Priority = (i == index) ? 10 : 5;
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
        int nextIndex = activeCameraIndex;
        do
        {
            nextIndex++;
            if (nextIndex >= cameraDataArray.Length)
                nextIndex = 1;
        } while (cameraDataArray[nextIndex].camera == null && nextIndex != activeCameraIndex);

        if (nextCameraImage != null)
            nextCameraImage.sprite = cameraDataArray[nextIndex].image;

        if (nextCameraLabel != null)
            nextCameraLabel.text = cameraDataArray[nextIndex].label;

        // Previous preview
        int prevIndex = activeCameraIndex;
        do
        {
            prevIndex--;
            if (prevIndex <= 0)
                prevIndex = cameraDataArray.Length - 1;
        } while (cameraDataArray[prevIndex].camera == null && prevIndex != activeCameraIndex);

        if (previousCameraImage != null)
            previousCameraImage.sprite = cameraDataArray[prevIndex].image;

        if (previousCameraLabel != null)
            previousCameraLabel.text = cameraDataArray[prevIndex].label;
    }

}
    