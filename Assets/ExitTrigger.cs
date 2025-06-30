using UnityEngine;

public class ShowPanelOnTrigger : MonoBehaviour
{
    public GameObject panelToShow;  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            panelToShow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelToShow.SetActive(false);
        }
    }
}
