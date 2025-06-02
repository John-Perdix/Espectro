using UnityEngine;
using System.Collections.Generic;

public class UITriggerManager : MonoBehaviour
{
    [System.Serializable]
    public class UITriggerEntry
    {
        public GameObject uiObject;
        public string triggerName;
        public GameObject objectToDisable; // New field

        private void Start()
        {
            if (uiObject != null)
                uiObject.SetActive(false);
            if (objectToDisable != null)
                objectToDisable.SetActive(true);
        }
    }

    public DialogueCameraManager dialogueCameraManager;
    public List<UITriggerEntry> uiTriggers = new List<UITriggerEntry>();

    private void OnEnable()
    {
        if (dialogueCameraManager != null)
            dialogueCameraManager.OnTriggerActivated += HandleTriggerActivated;
    }

    private void OnDisable()
    {
        if (dialogueCameraManager != null)
            dialogueCameraManager.OnTriggerActivated -= HandleTriggerActivated;
    }

    private void HandleTriggerActivated(string activatedTriggerName)
    {
        foreach (var entry in uiTriggers)
        {
            if (entry.uiObject != null)
                entry.uiObject.SetActive(entry.triggerName == activatedTriggerName);

            if (entry.objectToDisable != null)
                entry.objectToDisable.SetActive(entry.triggerName != activatedTriggerName);
        }
    }
}