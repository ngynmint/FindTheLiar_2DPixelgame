using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public GameObject talkButton; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            talkButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            talkButton.SetActive(false);
    }
}
