using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// tracks player progress to show notification 
public class PlayerProgressTracker : MonoBehaviour
{
    public TMP_Text doneTalkingTextField;
    public static PlayerProgressTracker Instance;
    public GameObject doneTalkingPanel;
    public string[] npcNames;
    private bool alreadyTriggered = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        doneTalkingPanel.SetActive(false);
        Debug.Log("this is:" + AllNPCsTalkedTo());
        Debug.Log("has saba talked to :" + GameManager.Instance.GetNPCState("Ava").hasTalkedToPlayer);
    }
    void Update()
    {
        if (!alreadyTriggered && AllNPCsTalkedTo())
        {
            alreadyTriggered = true;
            StartCoroutine(ShowDoneTalkingPanel());
        }
    }

    /*
     * Checks if all NPCs in the list have been talked to
     * Returns true if all NPCs have hasTalkedToPlayer set to true
     */
    public bool AllNPCsTalkedTo()
    {
        foreach (var npc in npcNames)
        {
            var state = GameManager.Instance.GetNPCState(npc);
            if (!state.hasTalkedToPlayer)
                return false;
        }
        return true;
    }

    /*
     * Shows all talked to panel with typewriter effect
     */
    IEnumerator ShowDoneTalkingPanel()
    {
        doneTalkingPanel.SetActive(true);

        string text = "You have talked to all suspects! Select your final choice in the Suspectlist under the menu.";
        doneTalkingTextField.text = "";

        foreach (char c in text)
        {
            doneTalkingTextField.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);
        doneTalkingPanel.SetActive(false);
    }

    public void ResetAndDestroy()
    {
        Instance = null;
        Destroy(gameObject);
    }


}
