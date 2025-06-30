using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public GameObject player; 

    void Start()
    {
        string spawnName = SpawnPointData.spawnPointName;

        if (!string.IsNullOrEmpty(spawnName))
        {
            GameObject spawnPoint = GameObject.Find(spawnName);
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
            else
            {
                Debug.LogWarning("Spawn point not found: " + spawnName);
            }
        }
    }
}
