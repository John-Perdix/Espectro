using UnityEngine;
using Unity.Cinemachine;

[System.Serializable]
public class CameraDialogueTrigger
{
    public CinemachineCamera camera;
    public string triggerName;
    public bool alreadyTriggered = false; // Optional: prevents repeat triggering
}
