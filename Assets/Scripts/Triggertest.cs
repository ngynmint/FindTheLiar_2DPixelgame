using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterTrigger : MonoBehaviour
{
    public TMP_Text targetText;   // Drag your canvas TMP Text here in Inspector
    public float typingSpeed = 0.05f;

    public void TriggerText(string who)
    {
        string message = "";

        switch (who)
        {
            case "Mia":
                lineToDisplay = "You shake her bag...";
                break;
            case "Chloe":
                lineToDisplay = "You shake her bag...";
                break;
            case "Ava":
                lineToDisplay = "You shake her bag...";
                break;
            case "Noah":
                lineToDisplay = "You pat him down...";
                break;
            case "Oliver":
                lineToDisplay = "You pat him down...";
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
    }
}
