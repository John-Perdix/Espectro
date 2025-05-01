using TMPro;
using UnityEngine;

public class Alertas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;

    // Optional: set a default message on start
    [SerializeField] private string defaultMessage = "Ready";

    private void Awake()
    {
        if (textComponent == null)
        {
            Debug.LogWarning("Text component not assigned. Attempting to auto-assign.");
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        SetText(defaultMessage);
    }

    /// <summary>
    /// Sets the text of the UI component.
    /// </summary>
    public void SetText(string newText)
    {
        if (textComponent != null)
        {
            textComponent.text = newText;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    /// <summary>
    /// Clears the text.
    /// </summary>
    public void ClearText()
    {
        SetText(string.Empty);
    }
}
