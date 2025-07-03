using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GroqAIService;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject talkButton;
    public GameObject confirmButton;
    public GameObject nextButton;

    public GameObject playerPanel;
    public TMP_InputField playerInput;

    public GameObject player;
    private PlayerMovement playerMovement;

    private NPC currentNPC;
    private int step = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        talkButton.SetActive(false);
        confirmButton.SetActive(false);
        nextButton.SetActive(false);
        playerPanel.SetActive(false);

        talkButton.GetComponent<Button>().onClick.AddListener(OnTalk);
        confirmButton.GetComponent<Button>().onClick.AddListener(OnPlayerConfirm);
        //nextButton.GetComponent<Button>().onClick.AddListener(NextStep);
        Debug.Log(step);
    }

    public void SetCurrentNPC(NPC npc)
    {
        currentNPC = npc;
        Debug.Log("NPC" + currentNPC + "has been set up");
    }


    void OnTalk()
    {
        playerMovement.canMove = false;
        talkButton.SetActive(false);
        if (step >= 5)
        {
            string response = AlreadyTalkedResponse(currentNPC.npcName);
            StartCoroutine(TypewriterEffect(currentNPC.npcTextField, response));
        }
        else
        {
        StartCoroutine(SendToGroq("")); 
        }
        ;
        
    }

    private string AlreadyTalkedResponse(string npcName)
    {
        switch (npcName)
        {
            case "Mia":
                return "I think I've already told you everything I know, sorry.";
            case "Noah":
                return "What do you call a ring that tells jokes? A “pun-dant!";
            case "Chloe":
                return "Thank you for your efforts to find the ring, we really appreciate it!";
            case "Ava":
                return "Still haven't found the thief, Sherlock?.";
            case "Oliver":
                return "I haven't seen anything else.";
            default:
                return "We've already talked.";
        }
    }

public GameObject ScrollViewPlayer;
    public void NextStep()
    {
        step++;
        Debug.Log(step);

        if (step == 1 || step == 3)
        {
            currentNPC.dialoguePanel.SetActive(false);
            currentNPC.npcTextField.text = "";
            dialogueScrollRect.verticalNormalizedPosition = 1f;

            playerPanel.SetActive(true);
            playerInput.text = "";
            ScrollViewPlayer.SetActive(true);
            playerInput.ActivateInputField();
            confirmButton.SetActive(true);
        }
        else if (step == 5)
        {
            GameManager.Instance.GetNPCState(currentNPC.npcName).hasTalkedToPlayer = true;
            EndDialogue();
        }
        nextButton.SetActive(false);

    }

    
    
    void OnPlayerConfirm()
    {
        step++;
        string userInput = playerInput.text.Trim();
        if (string.IsNullOrEmpty(userInput)) return;

        currentNPC.messageHistory.Add(("user", userInput));

        confirmButton.SetActive(false);
        ScrollViewPlayer.SetActive(false);
        playerPanel.SetActive(false);
        playerInput.text = "";
        StartCoroutine(SendToGroq(userInput));
    }

    IEnumerator SendToGroq(string prompt)
    {
        Debug.Log("Sending Prompt to AI");
        Debug.Log(prompt);
        if (step == 0)
            currentNPC.messageHistory.Add(("user", prompt));

        List<ChatMessage> formattedMessages = new List<ChatMessage>();
        foreach (var (role, content) in currentNPC.messageHistory)
        {
            formattedMessages.Add(new ChatMessage(role, content));
        }

        yield return GroqAIService.Instance.SendMessageToAI(formattedMessages, response =>
        {
            currentNPC.messageHistory.Add(("assistant", response));
            playerPanel.SetActive(false);
            currentNPC.dialoguePanel.SetActive(true);
            currentNPC.npcTextField.text = "";
            dialogueScrollRect.verticalNormalizedPosition = 0f;

            StartCoroutine(TypewriterEffect(currentNPC.npcTextField, response));
        });
    }


    public TMP_Text npcTextField;
    public ScrollRect dialogueScrollRect;
    IEnumerator TypewriterEffect(TMP_Text textComp, string fullText)
    {
        textComp.text = "";
        yield return null;

        for (int i = 0; i < fullText.Length; i++)
        {
            textComp.text += fullText[i];
            Canvas.ForceUpdateCanvases();

            dialogueScrollRect.verticalNormalizedPosition = 0f;

            yield return new WaitForSeconds(0.02f);
        }
            Debug.Log("Typing done");
            nextButton.SetActive(true);
    }

    void EndDialogue()
    {
        currentNPC.dialoguePanel.SetActive(false);
        currentNPC.npcTextField.text = "";
        playerPanel.SetActive(false);
        confirmButton.SetActive(false);
        nextButton.SetActive(false);
        playerMovement.canMove = true;
    }
}
