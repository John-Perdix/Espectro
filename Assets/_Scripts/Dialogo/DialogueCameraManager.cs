using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class DialogueCameraManager : MonoBehaviour
{
    public DialogoListas dialogo;
    public event System.Action<string> OnTriggerActivated;
    public List<CameraDialogueTrigger> cameraTriggers;
    public CinemachineSwitcherDynamic cameraSwitcher; // Assign in inspector

    private CinemachineBrain brain;

    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void Update()
{
    if (brain == null) return;

    var activeCam = brain.ActiveVirtualCamera as CinemachineVirtualCameraBase;

    foreach (var entry in cameraTriggers)
    {
        int camIndex = System.Array.IndexOf(cameraSwitcher.cameraDataArray, entry.cameraData);
        if (camIndex >= 0 && cameraSwitcher.cameraInstances[camIndex] == activeCam)
        {
            // Dialogue: only trigger once
            if (!entry.alreadyTriggered)
            {
                dialogo.StartDialogue(entry.triggerName);
                entry.alreadyTriggered = true;
            }
            // UI: always trigger
            if (OnTriggerActivated != null)
                OnTriggerActivated(entry.triggerName);
        }
    }
}
}