using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class DialogueCameraManager : MonoBehaviour
{
    public DialogoListas dialogo;  // Your dialogue handler
    public List<CameraDialogueTrigger> cameraTriggers;

    private CinemachineBrain brain;

    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if (brain == null) return;

        CinemachineCamera activeCam = brain.ActiveVirtualCamera as CinemachineCamera;

        foreach (var entry in cameraTriggers)
        {
            if (!entry.alreadyTriggered && activeCam == entry.camera)
            {
                dialogo.StartDialogue(entry.triggerName);
                entry.alreadyTriggered = true;
            }
        }
    }
}