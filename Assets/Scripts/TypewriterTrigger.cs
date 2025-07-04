using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterTrigger : MonoBehaviour
{
    public TMP_Text targetText;   
    public float typingSpeed = 0.05f;
    public EndSceneController endSceneController;
    private string suspectName;
    public void TriggerText(string who)
    {
        suspectName = who;
        string message = "";

        switch (who)
        {
            case "Mia":
                message = "You shake her bag...";
                break;
            case "Chloe":
                message = "You shake her bag...";
                break;
            case "Ava":
                message = "You shake her bag...";
                break;
            case "Noah":
                message = "You pat him down...";
                break;
            case "Oliver":
                message = "You pat him down...";
                break;
            default:
                message = "Unknown character.";
                break;
        }

        StopAllCoroutines();
        StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string message)
    {
        targetText.text = "";
        foreach (char c in message)
        {
            targetText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(2f);
    
        endSceneController.CheckResult(suspectName);
    }
}
