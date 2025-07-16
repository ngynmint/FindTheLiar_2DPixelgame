    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class SceneConnector2ndto1st : MonoBehaviour
    {
        public TMP_InputField playerInputScene1;
        public GameObject playerPanelScene1;
        public GameObject talkButtonScene1;
        public GameObject confirmButtonScene1;
        public GameObject nextButtonScene1;
        public GameObject ScrollViewPlayer1;
        public ScrollRect dialogueScrollRect1;
        public TMP_Text npcTextField1;
        
        public TMP_Text doneTalkingTextField1;
        public GameObject doneTalkingPanel1;

    public GameObject player1;
        public GameObject suspectList1;     
        public GameObject suspectListClickable1;
        public GameObject suspectListPanel1;
        void Start()
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.playerInput = playerInputScene1;
                DialogueManager.Instance.playerPanel = playerPanelScene1;
                DialogueManager.Instance.talkButton = talkButtonScene1;
                DialogueManager.Instance.confirmButton = confirmButtonScene1;
                DialogueManager.Instance.nextButton = nextButtonScene1;
                DialogueManager.Instance.ScrollViewPlayer = ScrollViewPlayer1;
                DialogueManager.Instance.dialogueScrollRect = dialogueScrollRect1;
                DialogueManager.Instance.npcTextField = npcTextField1;
                DialogueManager.Instance.suspectListPanel = suspectListPanel1;
                DialogueManager.Instance.player = player1;
                talkButtonScene1.GetComponent<Button>().onClick.AddListener(DialogueManager.Instance.OnTalk);
                
                confirmButtonScene1.GetComponent<Button>().onClick.AddListener(DialogueManager.Instance.OnPlayerConfirm);
                nextButtonScene1.GetComponent<Button>().onClick.AddListener(DialogueManager.Instance.NextStep);
                DialogueManager.Instance.playerMovement = player1.GetComponent<PlayerMovement>();
                DialogueManager.Instance.InitializeDialogueUI();
                
                PlayerProgressTracker.Instance.doneTalkingPanel = doneTalkingPanel1;
                PlayerProgressTracker.Instance.doneTalkingTextField = doneTalkingTextField1;
                SuspectListManager.Instance.suspectList = suspectList1;
                SuspectListManager.Instance.suspectListClickable = suspectListClickable1;
            }
            else
            {
                Debug.LogWarning("No DialogueManager found");
            }
        }
    }

