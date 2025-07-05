using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public void CheckResult(string chosenSuspect)
    {
        if (LiarChooser.Instance == null)
        {
            Debug.LogError("No LiarChooser instance found!");
            return;
        }

        string actualLiar = LiarChooser.Instance.liarName;
        Debug.Log($"Chosen: {chosenSuspect}, Actual: {actualLiar}");

        if (chosenSuspect == actualLiar)
        {
            SceneManager.LoadScene("WinScene"); 
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
