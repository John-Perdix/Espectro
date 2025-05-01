using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogoListas : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.05f;
    public DialogueDatabase dialogueDatabase;

    private Dictionary<string, string[]> dialoguesDict;
    private string[] currentLines;
    private int index;

    void Awake()
    {
        // Convert ScriptableObject list into a dictionary
        dialoguesDict = new Dictionary<string, string[]>();
        foreach (var entry in dialogueDatabase.dialogues)
        {
            dialoguesDict[entry.triggerName] = entry.lines;
        }

        textComponent.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentLines == null) return;

            if (textComponent.text == currentLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentLines[index];
            }
        }
    }

    public void StartDialogue(string triggerName)
    {
        if (!dialoguesDict.ContainsKey(triggerName))
        {
            Debug.LogWarning($"Dialogue not found for trigger '{triggerName}'");
            return;
        }

        currentLines = dialoguesDict[triggerName];
        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
