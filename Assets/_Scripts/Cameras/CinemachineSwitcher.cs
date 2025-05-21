using UnityEngine;
using Unity.Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineCamera[] virtualCameras;
    //public CinemachineSwitcherDynamic cinemachineSwitcherDynamic;
    public int activeCameraIndex = 0;

    private void Awake()
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
        //cinemachineSwitcherDynamic.UpdateUI();
    }


    public void IncrementCameraIndex()
    {
        int nextIndex = (activeCameraIndex + 1) % virtualCameras.Length;
        ActivateCamera(nextIndex);
    }

    public void DecrementCameraIndex()
    {
        int prevIndex = (activeCameraIndex - 1 + virtualCameras.Length) % virtualCameras.Length;
        ActivateCamera(prevIndex);
    }
}

