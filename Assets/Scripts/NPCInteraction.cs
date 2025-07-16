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
    public GameObject idleNPC;
    public GameObject TalkingNPC;

    public GameObject dialoguePanel;
    public TMP_Text npcTextField;
    public GameObject npcNotes;
    public AudioSource dialogueAudio;


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
                basePrompt += "\nYou are the thief and the liar. Never give up your identity as the thief and liar, when asked or threatened. Dont randomly make stuff up. Your lies should be contradicting to your character, and make it obvious. Behave the opposite way than what your charactertraits are in an obvious way. (for example saying you did something that according to your charactertraits you don't usally do) and your statements should contradict the infos on the setting but not on where you are rn. Make it obvious through the contradictions to guess that you are the liar.Your first response rigth now should be as if the player said Hi to you to initiate a conversation. Just say hi back in your character Stay in character!";
            }
            else
            {
                basePrompt += "\nYou are not the thief. Don't Blame anyone else randomly to protect yourself, just stay in Character and you CAN but DON'T HAVE TO USE ALL of the charactertraits to seem honest to the Player. Your first response rigth now should be as if the player said Hi to you to initiate a conversation. Just say hi back in your character Stay in character!";
            }
            messageHistory = new List<(string role, string content)> { ("system", basePrompt) };
            state.messageHistory = messageHistory; // Save the new history back
        }
    }

    public void SetIsTalking(bool isTalking)
{
    if (idleNPC != null)
        idleNPC.SetActive(!isTalking);

    if (TalkingNPC != null)
        TalkingNPC.SetActive(isTalking);
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
