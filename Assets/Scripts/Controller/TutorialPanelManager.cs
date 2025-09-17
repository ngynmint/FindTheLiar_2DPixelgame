using UnityEngine;

public class TutorialPanelManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    private static bool hasSeenTutorialThisSession = false;

    /*
    * Displays a tutorial panel on first time playing the game and ensures, it only pops up once.
    */
    private void Start()
    {
        if (!hasSeenTutorialThisSession)
        {
            tutorialPanel.SetActive(true);
            hasSeenTutorialThisSession = true;
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
