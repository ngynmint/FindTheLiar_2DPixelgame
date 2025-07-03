using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTriggerStairs : MonoBehaviour
{
    public string sceneToLoad; 
    public NPC npcInstance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnPointData.spawnPointName = "SpawnPointStairs";
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
