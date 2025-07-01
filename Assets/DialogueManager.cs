using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Panels")]
    public GameObject npcDialoguePanel;
    public TMP_Text npcDialogueText;
    public GameObject playerDialoguePanel;
    public TMP_InputField playerInputField;

    public Button confirmButton;
    public GameObject talkButton;

    private NPC currentNPC;

    private int dialogueStep;
    private bool isPlayerTurn;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        npcDialoguePanel.SetActive(false);
        playerDialoguePanel.SetActive(false);
        talkButton.SetActive(false);

        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    public void SetCurrentNPC(NPC npc)
    {
        currentNPC = npc;
    }

    public void ClearCurrentNPC()
    {
        currentNPC = null;
    }

    public void StartDialogue()
    {
        if (currentNPC == null) return;

        dialogueStep = 0;
        isPlayerTurn = false;
        talkButton.SetActive(false);
        playerMovement.canMove = false;

        ShowNpcDialogue();
    }

    private void ShowNpcDialogue()
    {
        if (dialogueStep >= currentNPC.npcDialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        npcDialoguePanel.SetActive(true);
        playerDialoguePanel.SetActive(false);
        npcDialogueText.text = currentNPC.npcDialogueLines[dialogueStep];
        confirmButton.gameObject.SetActive(true);
    }

    private void ShowPlayerInput()
    {
        npcDialoguePanel.SetActive(false);
        playerDialoguePanel.SetActive(true);
        playerInputField.text = "";
        playerInputField.ActivateInputField();
        confirmButton.gameObject.SetActive(true);
    }

    private void OnConfirmClicked()
    {
        if (currentNPC == null) return;

        if (!isPlayerTurn)
        {
            // NPC turn finished, now player's turn
            isPlayerTurn = true;
            ShowPlayerInput();
        }
        else
        {
            // Player entered text, now NPC turn (next line)
            string playerResponse = playerInputField.text;
            Debug.Log($"Player said: {playerResponse}");
            // You can store playerResponse or send to GPT here

            isPlayerTurn = false;
            dialogueStep++;

            ShowNpcDialogue();
        }
    }

    private void EndDialogue()
    {
        npcDialoguePanel.SetActive(false);
        playerDialoguePanel.SetActive(false);
        talkButton.SetActive(false);
        playerMovement.canMove = true;
        currentNPC = null;
    }
}
