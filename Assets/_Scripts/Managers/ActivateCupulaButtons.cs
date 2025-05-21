using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Linq; // Add this for easier list manipulation

public class ActivateCupulaButtons : MonoBehaviour
{
    [SerializeField] CanvasToggle cupulaButtons;
    public DialogoListas dialogoListas;
    public DialogueCameraManager dialogueCameraManager;
    [SerializeField] string targetTriggerName; // The name of the trigger you're interested in

    void Update()
    {
        if (dialogoListas != null && dialogoListas.index == 0 && !dialogoListas.gameObject.activeInHierarchy && dialogueCameraManager != null)
        {
            // Find the specific CameraDialogueTrigger in the list
            CameraDialogueTrigger targetEntry = dialogueCameraManager.cameraTriggers.FirstOrDefault(entry => entry.triggerName == targetTriggerName);

            if (targetEntry != null && targetEntry.alreadyTriggered)
            {
                cupulaButtons.Show();
                Debug.Log("Cupula buttons should be shown");
            }
        }
    }
}