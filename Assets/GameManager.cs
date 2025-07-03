using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<NPCState> npcStates = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public NPCState GetNPCState(string npcName)
    {
        var state = npcStates.Find(n => n.npcName == npcName);
        if (state == null)
        {
            state = new NPCState { npcName = npcName };
            npcStates.Add(state);
        }
        return state;
    }


    [System.Serializable]
    public class NPCState
    {
        public string npcName;
        public List<(string role, string content)> messageHistory = new();
        public bool hasTalkedToPlayer = false;
    }

}

