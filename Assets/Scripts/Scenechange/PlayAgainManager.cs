using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainManager : MonoBehaviour
{
    public string sceneName;

    public void OnPlayAgain()
    {
        Debug.Log("Play Again button clicked!");
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
            GameManager.Instance = null;
        }

        if (PlayerProgressTracker.Instance != null)
        {
            Destroy(PlayerProgressTracker.Instance.gameObject);
            PlayerProgressTracker.Instance = null;
        }

        if (DialogueManager.Instance != null)
        {
            Destroy(DialogueManager.Instance.gameObject);
            DialogueManager.Instance = null;
        }

         if (SuspectListManager.Instance != null)
        {
            Destroy(SuspectListManager.Instance.gameObject);
            SuspectListManager.Instance = null;
        }

          if (EndSceneController.Instance != null)
        {
            Destroy(EndSceneController.Instance.gameObject);
            EndSceneController.Instance = null;
        }

        if (LiarChooser.Instance != null)
        {
            Destroy(LiarChooser.Instance.gameObject);
            LiarChooser.Instance = null;
        }

        if (BackgroundMusic.Instance != null)
        {
            Destroy(BackgroundMusic.Instance.gameObject);
            BackgroundMusic.Instance = null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
