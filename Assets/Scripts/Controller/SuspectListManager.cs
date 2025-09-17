using UnityEngine;

public class SuspectListManager : MonoBehaviour
{
    public SuspectIconToggle[] suspectIcons; //array of suspect icons to update

    [Header("Panels")]
    public GameObject suspectList; 
    public GameObject suspectListClickable;

    public static SuspectListManager Instance;
    private bool switched = false; // so that the suspectlist doesn't swap back anymore, once updated

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

    /*
     * switches to clickable suspectlist for final selection
     */
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
    
    public void ResetAndDestroy()
    {
        Instance = null;
        Destroy(gameObject);
    }


}
