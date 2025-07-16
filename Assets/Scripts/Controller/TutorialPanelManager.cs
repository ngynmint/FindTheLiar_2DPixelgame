using UnityEngine;

public class TutorialPanelManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    // Static variable = shared across all instances, and resets when the game restarts
    private static bool hasSeenTutorialThisSession = false;

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
