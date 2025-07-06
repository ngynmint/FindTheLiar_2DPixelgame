using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public static EndSceneController Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
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
            SceneManager.LoadScene("LostScene");
        }
    }
    public void ResetAndDestroy()
    {
        Instance = null;
        Destroy(gameObject);
    }

}
