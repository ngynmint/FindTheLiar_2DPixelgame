using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string npcName;
    [TextArea(3, 10)]
    public string systemPrompt;

    public GameObject dialoguePanel;
    public TMP_Text npcTextField;

    [HideInInspector] public List<(string role, string content)> messageHistory = new();

    private void Start()
    {
        DialogueManager.Instance.SetCurrentNPC(this);
        var state = GameManager.Instance.GetNPCState(npcName);
        if (state.messageHistory.Count == 0)
        {
            string basePrompt = systemPrompt;
            if (LiarChooser.Instance != null && LiarChooser.Instance.IsLiar(npcName))
            {
                basePrompt += "\nYou are the thief, but have to lie in order to not get caught. Use your character traits to help contradict your statements, so that the player gets clues about you being the liar. But never give up your identity.";
            }
            else
            {
                basePrompt += "\nYou are not the thief. Stay true to your character traits and do not lie.";
            }
            state.messageHistory.Add(("system", basePrompt));
        }

        messageHistory = state.messageHistory;

    }

    public void ResetConversation()
    {
        messageHistory.Clear();
        messageHistory.Add(("system", systemPrompt));
    }
    
}
