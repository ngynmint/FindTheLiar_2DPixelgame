using UnityEngine;

public class SuspectListManager : MonoBehaviour
{
    public SuspectIconToggle[] suspectIcons;

    [Header("Panels")]
    public GameObject suspectList;     
    public GameObject suspectListClickable;    

    private bool switched = false;

    void Start()
    {
        if (suspectListClickable != null)
            suspectListClickable.SetActive(false);
    }

    void Update()
    {
        if (!switched && PlayerProgressTracker.Instance != null && PlayerProgressTracker.Instance.AllNPCsTalkedTo())
        {
            SwitchSuspectLists();
        }
        
    }

    private void SwitchSuspectLists()
    {
        switched = true;
        if (suspectList != null)
        {
            suspectList.SetActive(false);
        }
        
        if (suspectListClickable != null)
        {
            suspectListClickable.SetActive(true);
        }

    }
    public void UpdateAllSuspectIcons()
    {
        foreach (var icon in suspectIcons)
        {
            icon.UpdateIcon();
        }
    }

}
