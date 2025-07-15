using UnityEngine;

public class TutorialPanelManager: MonoBehaviour
{
    public GameObject tutorialPanel;

    private void Start()
    {
        if (PlayerPrefs.GetInt("HasSeenTutorial", 0) == 0)
        {
            tutorialPanel.SetActive(true); 
        }
        else
        {
            tutorialPanel.SetActive(false); 
        }
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("HasSeenTutorial", 1);
        PlayerPrefs.Save();
    }
}
