using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDialogueDatabase", menuName = "Dialogue/Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public List<DialogueEntry> dialogues;
}