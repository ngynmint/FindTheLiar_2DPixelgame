using UnityEngine;

public class NPC : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] npcDialogueLines;  // Hardcoded or prefilled lines for this NPC
    public GameObject npcDialoguePanel;

    public GameObject TalkTriggerBox;

    public string npcName; // Optional, for reference

    // Other NPC specific data can go here, e.g. ID, portrait sprite, etc.
}
