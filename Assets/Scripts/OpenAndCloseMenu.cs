using UnityEngine;

public class MenubuttonManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] otherPanels;

     public void OnMenuButtonClick()
    {
        bool anyOtherOpen = false;

        foreach (GameObject panel in otherPanels)
        {
            if (panel != null && panel.activeSelf)
            {
                anyOtherOpen = true;
                break;
            }
        }

        if (anyOtherOpen)
        {
            Debug.LogWarning("smthgs open");
            foreach (GameObject panel in otherPanels)
            {
                panel.SetActive(false);
            }
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
