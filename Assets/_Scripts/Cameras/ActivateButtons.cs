using UnityEngine;
using Unity.Cinemachine;


public class ActivateButtons : MonoBehaviour
{
    private CinemachineVirtualCameraBase vCam;
    public CanvasToggle butoes; // Make this public so you can assign it in the Inspector

    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCameraBase>();
    }

    void Update()
    {
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        if (brain == null || vCam == null || butoes == null)
            return;

        bool isActive = brain.ActiveVirtualCamera == vCam;

        if (isActive)
        {
            butoes.Show();
        }
        else
        {
            butoes.Hide(); // Or whatever method you want to use when not active
        }
    }
}
