using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneConnector : MonoBehaviour
{
    public TMP_InputField playerInputScene2;
    public GameObject playerPanelScene2;
    public GameObject talkButtonScene2;
    public GameObject confirmButtonScene2;
    public GameObject nextButtonScene2;
    public GameObject ScrollViewPlayer2;
    public ScrollRect dialogueScrollRect2;
    public TMP_Text npcTextField2;
    public GameObject player2;
    public NPC currentNPC2;

    void Start()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.playerInput = playerInputScene2;
            DialogueManager.Instance.playerPanel = playerPanelScene2;
            DialogueManager.Instance.talkButton = talkButtonScene2;
            DialogueManager.Instance.confirmButton = confirmButtonScene2;
            DialogueManager.Instance.nextButton = nextButtonScene2;
            DialogueManager.Instance.ScrollViewPlayer = ScrollViewPlayer2;
            DialogueManager.Instance.dialogueScrollRect = dialogueScrollRect2;
            DialogueManager.Instance.npcTextField = npcTextField2;
            DialogueManager.Instance.player = player2;
            talkButtonScene2.GetComponent<Button>().onClick.RemoveAllListeners();
            talkButtonScene2.GetComponent<Button>().onClick.AddListener(DialogueManager.Instance.OnTalk);
            confirmButtonScene2.GetComponent<Button>().onClick.RemoveAllListeners();
            confirmButtonScene2.GetComponent<Button>().onClick.AddListener(DialogueManager.Instance.OnPlayerConfirm);
            nextButtonScene2.GetComponent<Button>().onClick.RemoveAllListeners();
            nextButtonScene2.GetComponent<Button>().onClick.AddListener(DialogueManager.Instance.NextStep);
            DialogueManager.Instance.playerMovement = player2.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogWarning("No DialogueManager found");
        }
    }
}

