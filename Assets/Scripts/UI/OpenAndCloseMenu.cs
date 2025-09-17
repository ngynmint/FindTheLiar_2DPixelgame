using UnityEngine;

public class MenubuttonManager : MonoBehaviour
{
    public GameObject menuPanel;

    public void OnMenuButtonClick()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        Debug.Log("Ich");
    }
}
