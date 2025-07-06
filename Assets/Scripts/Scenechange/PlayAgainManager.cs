using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainManager : MonoBehaviour
{
    public string sceneName;

    public void OnPlayAgain()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResetAndDestroy();

        if (PlayerProgressTracker.Instance != null)
            PlayerProgressTracker.Instance.ResetAndDestroy();
            
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.ResetAndDestroy();

        if (SuspectListManager.Instance != null)
            SuspectListManager.Instance.ResetAndDestroy();

        if (EndSceneController.Instance != null)
            EndSceneController.Instance.ResetAndDestroy();

        if (LiarChooser.Instance != null)
            LiarChooser.Instance.ResetAndDestroy();

        SceneManager.LoadScene(sceneName);
    }
}
