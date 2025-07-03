using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [HideInInspector]
    public int dialogueStep = 0;
    public string npcName;
    [TextArea(3, 10)]
    public string systemPrompt;

    public GameObject dialoguePanel;
    public TMP_Text npcTextField;
    public GameObject npcNotes;

    [HideInInspector] public List<(string role, string content)> messageHistory = new();

    private void Start()
    {
        var state = GameManager.Instance.GetNPCState(npcName);
        dialogueStep = state.dialogueStep;
        messageHistory = state.messageHistory;
        if (messageHistory == null || messageHistory.Count == 0)
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
            messageHistory = new List<(string role, string content)> { ("system", basePrompt) };
            state.messageHistory = messageHistory; // Save the new history back
        }
    }

    public void SaveState()
    {
        var state = GameManager.Instance.GetNPCState(npcName);
        state.dialogueStep = dialogueStep;
        state.messageHistory = new List<(string role, string content)>(messageHistory);
    }


    public void ResetConversation()
    {
        messageHistory.Clear();
        messageHistory.Add(("system", systemPrompt));
    }
    
}
