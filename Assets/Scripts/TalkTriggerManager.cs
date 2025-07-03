using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public GameObject talkButton;
    public NPC npc;

    private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Entered trigger with: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered NPC trigger");
            DialogueManager.Instance.SetCurrentNPC(npc);
            talkButton.SetActive(true);
    }
}


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
           talkButton.SetActive(false);
    }
}
