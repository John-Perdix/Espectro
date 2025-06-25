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

    [Header("Navigation Buttons")]
    public Button botaoAnterior; // Previous button
    public Button botaoSeguinte;


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

        // Enable/disable navigation buttons
        if (botaoAnterior != null)
            botaoAnterior.interactable = activeCameraIndex > 1;

        if (botaoSeguinte != null)
            botaoSeguinte.interactable = activeCameraIndex < cameraDataArray.Length - 1;

        // Next preview - only fill if button is enabled
        if (botaoSeguinte != null && botaoSeguinte.interactable)
        {
            int nextIndex = (activeCameraIndex + 1) % cameraDataArray.Length;
            while (cameraDataArray[nextIndex].camera == null && nextIndex != activeCameraIndex)
                nextIndex = (nextIndex + 1) % cameraDataArray.Length;

            if (nextCameraImage != null)
            {
                nextCameraImage.gameObject.SetActive(true);
                nextCameraImage.sprite = cameraDataArray[nextIndex].image;
            }

            if (nextCameraLabel != null)
                nextCameraLabel.text = cameraDataArray[nextIndex].label;
        }
        else
        {
            // Clear and deactivate next camera preview when button is disabled
            if (nextCameraImage != null)
            {
                nextCameraImage.sprite = null;
                nextCameraImage.gameObject.SetActive(false);
            }

            if (nextCameraLabel != null)
                nextCameraLabel.text = "";
        }

        // Previous preview - only fill if button is enabled
        if (botaoAnterior != null && botaoAnterior.interactable)
        {
            int prevIndex = (activeCameraIndex - 1 + cameraDataArray.Length) % cameraDataArray.Length;
            while (cameraDataArray[prevIndex].camera == null && prevIndex != activeCameraIndex)
                prevIndex = (prevIndex - 1 + cameraDataArray.Length) % cameraDataArray.Length;

            if (previousCameraImage != null)
            {
                previousCameraImage.gameObject.SetActive(true);
                previousCameraImage.sprite = cameraDataArray[prevIndex].image;
            }

            if (previousCameraLabel != null)
                previousCameraLabel.text = cameraDataArray[prevIndex].label;
        }
        else
        {
            // Clear and deactivate previous camera preview when button is disabled
            if (previousCameraImage != null)
            {
                previousCameraImage.sprite = null;
                previousCameraImage.gameObject.SetActive(false);
            }

            if (previousCameraLabel != null)
                previousCameraLabel.text = "";
        }
    }

}
