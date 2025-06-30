using UnityEngine;

public class ShowPanelOnTrigger : MonoBehaviour
{
    public GameObject panelToShow;
    public GameObject buttonToHide;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelToShow.SetActive(true);
            if (buttonToHide != null)
                buttonToHide.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelToShow.SetActive(false);
            if (buttonToHide != null)
                buttonToHide.SetActive(true);
        }
    }
}
