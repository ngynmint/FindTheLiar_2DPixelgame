using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SpawnPointData.spawnPointName = "SpawnPointDoor";
        SceneManager.LoadScene(sceneName);
    }
}
