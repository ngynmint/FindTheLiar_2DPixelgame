using UnityEngine;

public class SuspectListManager : MonoBehaviour
{
    public SuspectIconToggle[] suspectIcons; 

    public void UpdateAllSuspectIcons()
    {
        foreach (var icon in suspectIcons)
        {
            icon.UpdateIcon();
        }
    }

}
