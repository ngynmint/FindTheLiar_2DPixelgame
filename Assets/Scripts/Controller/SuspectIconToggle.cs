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

    public void UpdateIcon()
    {
        bool hasTalked = GameManager.Instance.GetNPCState(npcName).hasTalkedToPlayer;

        iconNotTalkedTo.SetActive(!hasTalked);
        iconTalkedTo.SetActive(hasTalked);
    }
}
