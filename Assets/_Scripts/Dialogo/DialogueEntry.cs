using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string triggerName;

    [TextArea(2, 10)]
    public string[] lines;
}