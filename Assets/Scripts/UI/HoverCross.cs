using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuspectButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject redXIcon; 
    public string suspectName;
    public GameObject blackBackground;
    public static TypewriterTrigger typewriterTrigger;

    public void OnPointerEnter(PointerEventData eventData)
    {
        redXIcon.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        redXIcon.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Player chose: " + suspectName);
        PlayerPrefs.SetString("ChosenSuspect", suspectName);
        blackBackground.SetActive(true);
        typewriterTrigger.TriggerText(suspectName);
    }
}
