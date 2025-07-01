using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject talkButton;
    private NPC npc;

    private void Start()
    {
        npc = GetComponentInParent<NPC>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkButton.SetActive(true);
            DialogueManager.Instance.SetCurrentNPC(npc);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkButton.SetActive(false);
            DialogueManager.Instance.ClearCurrentNPC();
        }
    }
}
