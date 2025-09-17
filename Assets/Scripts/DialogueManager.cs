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
    public GameObject talkButton; //button to initiate conv
    private Coroutine currentTypingCoroutine;

    public GameObject confirmButton; //button to confirm player input
    public GameObject nextButton; //button to jump to next dialogue step

    public GameObject playerPanel; 
    public TMP_InputField playerInput;

    public GameObject player; 
    [HideInInspector]
    public PlayerMovement playerMovement;
    public GameObject suspectListPanel; 

    private NPC currentNPC;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    /*
    * allows player to press enter for confirmation as well
    */
    private void Update()
    {
        if (playerPanel.activeSelf && playerInput.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            OnPlayerConfirm();
        }
    }

    /*
    * Initialize and sets up Buttons and Listeners
    */
    public void InitializeDialogueUI()
    {
        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>();

        if (talkButton != null)
        {
            talkButton.SetActive(false);
            talkButton.GetComponent<Button>().onClick.RemoveAllListeners();
            talkButton.GetComponent<Button>().onClick.AddListener(OnTalk);
        }

        if (confirmButton != null)
        {
            confirmButton.SetActive(false);
            confirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
            confirmButton.GetComponent<Button>().onClick.AddListener(OnPlayerConfirm);
        }

        if (playerPanel != null)
            playerPanel.SetActive(false);
    }

    public void SetCurrentNPC(NPC npc)
    {
        currentNPC = npc;
        Debug.Log("NPC" + currentNPC + "has been set up");
    }

    /*
    * Upon pressing talkbutton, starts dialogue, updates the UI (suspectlist disappears, NPC talking panel, 
    * stops player movement) and sends the system prompt of the NPC to the API.
    * If already talked to, just display automated response, instead of AI response.
    */
    public void OnTalk()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }
        if (currentNPC == null)
        {
            Debug.LogError("Current NPC is null!");
            return;
        }
        if (string.IsNullOrEmpty(currentNPC.npcName))
        {
            Debug.LogError("Current NPC's npcName is null or empty!");
            return;
        }

        suspectListPanel.SetActive(false);
        currentNPC.dialogueStep = GameManager.Instance.GetNPCState(currentNPC.npcName).dialogueStep;
        playerMovement.canMove = false;
        talkButton.SetActive(false);
        if (GameManager.Instance.GetNPCState(currentNPC.npcName).hasTalkedToPlayer == true)
        {
            dialogueScrollRect.verticalNormalizedPosition = 1f;
            currentNPC.dialoguePanel.SetActive(true);
            string response = AlreadyTalkedResponse(currentNPC.npcName);
            StartCoroutine(TypewriterEffect(currentNPC.npcTextField, response));
            playerMovement.canMove = true;
        }
        else
        {
            if (currentNPC.dialogueAudio != null)
            {
                currentNPC.dialogueAudio.Play();
            }
            StartCoroutine(SendToGroq(""));
        }
        ;
    }

        
    /*
    * Sends the string to the LLM API.
    * The first string gets added to the NPC's message history as a system prompt, and before sending, the message history
    * is being formatted into a tupel for groq. After sending and receiving response, updates UI (disable player panel,
    * activate NPC panel, NPC notes), adds response to the message history and displays it with typewriter effect.
    */
    IEnumerator SendToGroq(string prompt)
    {
        Debug.Log("Sending Prompt to AI");
        Debug.Log(prompt);
        if (currentNPC.dialogueStep == 0)
            currentNPC.messageHistory.Add(("system", prompt));

        List<ChatMessage> formattedMessages = new List<ChatMessage>(); //umwandlung in tupeln nötig...
        foreach (var (role, content) in currentNPC.messageHistory)
        {
            formattedMessages.Add(new ChatMessage(role, content));
        }

        yield return GroqAIService.Instance.SendMessageToAI(formattedMessages, response => //callback, waits for response
        {
            currentNPC.SetIsTalking(true);
            currentNPC.messageHistory.Add(("assistant", response));
            playerPanel.SetActive(false);
            currentNPC.dialoguePanel.SetActive(true);
            currentNPC.npcNotes.SetActive(true);
            if (currentTypingCoroutine != null)
            StopCoroutine(currentTypingCoroutine);
            currentNPC.npcTextField.text = "";
            dialogueScrollRect.verticalNormalizedPosition = 0f;

            StartCoroutine(TypewriterEffect(currentNPC.npcTextField, response));
        });
    }

    /*
    * Prewritten NPC responses, if already talked to.
    */
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
                return "Still haven't found the thief, Sherlock?";
            case "Oliver":
                return "I haven't seen anything else.";
            default:
                return "We've already talked.";
        }

    }

    public GameObject ScrollViewPlayer;

    /*
    * Handles clicking the Next Button after an NPC's response. If player has not asked 3 questions yet, updates UI
    * (disable NPC panels and button, display player panelm inputfield and confirmationbutton), increases dialoguestep counter
    * and syncs it. 
    * If end of dialogue is reached, change NPC State and end dialogue.
    */
    public void NextStep()
    {
        currentNPC.dialogueStep++;
        Debug.Log(currentNPC.dialogueStep);
        GameManager.Instance.GetNPCState(currentNPC.npcName).dialogueStep = currentNPC.dialogueStep;

        if (currentNPC.dialogueStep == 1 || currentNPC.dialogueStep == 3 || currentNPC.dialogueStep == 5)
        {
            currentNPC.dialoguePanel.SetActive(false);
            currentNPC.npcTextField.text = "";
            dialogueScrollRect.verticalNormalizedPosition = 1f;

            currentNPC.SetIsTalking(false);
            playerPanel.SetActive(true);
            playerInput.text = "";
            ScrollViewPlayer.SetActive(true);
            playerInput.ActivateInputField();
            confirmButton.SetActive(true);
        }
        else if (currentNPC.dialogueStep == 7)
        {
            GameManager.Instance.GetNPCState(currentNPC.npcName).hasTalkedToPlayer = true;
            EndDialogue();
        }
        nextButton.SetActive(false);

    }

    /*
    * Handles confirmation after player input. Increases dialoguestep counter and syncs it, adds input to messagehistory
    * of the NPC, updates UI (disables player panel and confirmation button) and sends it to the LLM.
    */
    public void OnPlayerConfirm()
    {
        currentNPC.dialogueStep++;
        GameManager.Instance.GetNPCState(currentNPC.npcName).dialogueStep = currentNPC.dialogueStep;
        string userInput = playerInput.text.Trim();
        if (string.IsNullOrEmpty(userInput)) return;

        currentNPC.messageHistory.Add(("user", userInput));

        confirmButton.SetActive(false);
        ScrollViewPlayer.SetActive(false);
        playerPanel.SetActive(false);
        playerInput.text = "";
        StartCoroutine(SendToGroq(userInput));
    }
   


    public TMP_Text npcTextField;
    public ScrollRect dialogueScrollRect;

    /*
    * displays texts with a typewriter effect, activates NPC noises and displays the next step button after finishing couroutine
    */
    IEnumerator TypewriterEffect(TMP_Text textComp, string fullText)
    {
        textComp.text = "";
        yield return null;
        Debug.Log("Content height before: " + dialogueScrollRect.content.rect.height);
        Debug.Log("Viewport height before: " + dialogueScrollRect.viewport.rect.height);

        if (currentNPC.dialogueAudio != null)
        {
            currentNPC.dialogueAudio.Play();
        }


        for (int i = 0; i < fullText.Length; i++)
        {
            textComp.text += fullText[i];

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(npcTextField.rectTransform);

            //dialogueScrollRect.verticalNormalizedPosition = 0f;

            yield return new WaitForSeconds(0.02f);
        }
        Debug.Log("Content height after: " + dialogueScrollRect.content.rect.height);
        Debug.Log("Viewport height after: " + dialogueScrollRect.viewport.rect.height);
        currentNPC.SetIsTalking(false);

        if (currentNPC.dialogueAudio != null && currentNPC.dialogueAudio.isPlaying)
        {
            currentNPC.dialogueAudio.Stop();
        }
        yield return null;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(npcTextField.rectTransform);

        Debug.Log("Typing done.");

        if (currentNPC.dialogueStep < 7)
        {
            nextButton.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            currentNPC.dialoguePanel.SetActive(false);
            if (currentNPC.dialogueAudio != null && currentNPC.dialogueAudio.isPlaying)
            {
                currentNPC.dialogueAudio.Stop();
            }
            currentNPC.npcTextField.text = "";
        }

    }

    /*
    * After three prompts, handles the end of the dialogue. Update UI (disable NPC and player panels, enable player movement,
    * show suspectList again)
    */
    void EndDialogue()
    {
        Debug.Log("reched");
        currentNPC.dialoguePanel.SetActive(false);
        currentNPC.npcTextField.text = "";
        playerPanel.SetActive(false);
        currentNPC.npcNotes.SetActive(false);
        confirmButton.SetActive(false);
        nextButton.SetActive(false);
        playerMovement.canMove = true;
        currentNPC.SetIsTalking(false);
        suspectListPanel.SetActive(true);
        Debug.Log(PlayerProgressTracker.Instance.AllNPCsTalkedTo());
    }

    public void ResetAndDestroy()
    {
        Instance = null;
        Destroy(gameObject);
    }

}
