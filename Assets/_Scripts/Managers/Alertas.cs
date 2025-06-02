using TMPro;
using UnityEngine;
using System.Collections;

public class Alertas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string defaultMessage = "Ready";
    [SerializeField] private float hideDelay = 15f; // Time in seconds before the alert hides
    [SerializeField] private GameObject TextContainer;



    private Coroutine hideCoroutine;

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
            gameObject.SetActive(true);

            // Restart the hide coroutine
            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);
            hideCoroutine = StartCoroutine(HideAfterDelay());
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        if (transform.parent != null)
            TextContainer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Clears the text.
    /// </summary>
    public void ClearText()
    {
        SetText(string.Empty);
    }
}
