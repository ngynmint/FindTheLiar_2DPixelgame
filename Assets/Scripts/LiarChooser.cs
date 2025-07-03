using System.Collections.Generic;
using UnityEngine;

public class LiarChooser : MonoBehaviour
{
    public static LiarChooser Instance;

    public string[] npcNames = { "Ava", "Oliver" };
    public string liarName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PickRandomLiar();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PickRandomLiar()
    {
        int randomIndex = Random.Range(0, npcNames.Length);
        liarName = npcNames[randomIndex];
        Debug.Log("Liar selected: " + liarName);
    }

    public bool IsLiar(string npcName)
    {
        return npcName == liarName;
    }
}
