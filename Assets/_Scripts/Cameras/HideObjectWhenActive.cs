using UnityEngine;
using Unity.Cinemachine;

public class HideObjectWhenActive : MonoBehaviour
{
    public GameObject objectToHide; // Assign in Inspector

    private CinemachineVirtualCameraBase vCam;

    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCameraBase>();
    }

    void Update()
    {
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        if (brain == null || vCam == null || objectToHide == null)
            return;

        bool isActive = brain.ActiveVirtualCamera == vCam;
        objectToHide.SetActive(!isActive);
    }
}
