using UnityEngine;

public class SuspectIconToggle : MonoBehaviour
{
    public string npcName;
    public GameObject iconNotTalkedTo;
    public GameObject iconTalkedTo;

    void OnEnable()
    {
        UpdateIcon();
    }

    /*
     * Updates NPC icons dynamically depending on NPCState
     */
    public void UpdateIcon()
    {
        bool hasTalked = GameManager.Instance.GetNPCState(npcName).hasTalkedToPlayer;

        iconNotTalkedTo.SetActive(!hasTalked);
        iconTalkedTo.SetActive(hasTalked);

    }
}
