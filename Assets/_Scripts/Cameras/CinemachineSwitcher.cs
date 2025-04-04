using UnityEngine;
using Unity.Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineCamera[] virtualCameras;
    public int activeCameraIndex = 0;

    private void Start()
    {
        ActivateCamera(activeCameraIndex);
    }

    public void ActivateCamera(int index)
    {
        if (index < 0 || index >= virtualCameras.Length)
        {
            Debug.LogWarning("Invalid camera index");
            return;
        }

        // Set all cameras to low priority
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = (i == index) ? 10 : 5;
        }

        activeCameraIndex = index;
    }

    // Optional: Keyboard cycling through cameras
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int next = (activeCameraIndex + 1) % virtualCameras.Length;
            ActivateCamera(next);
        }
    }
}

