using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    private GameObject playerInstance;
    private TopDownCamera cameraFollow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerSpawnPoint(Vector3 spawnPosition)
    {
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            cameraFollow = Camera.main.GetComponent<TopDownCamera>();

            if (cameraFollow != null)
            {
                cameraFollow.playerTransform = playerInstance.transform;
            }
            else
            {
                Debug.LogError("CameraFollow script not found on the main camera.");
            }
        }
    }

    public void RegisterBunny(GameObject bunny)
    {
        bnuuyFollow bunnyFollow = bunny.GetComponent<bnuuyFollow>();
        if (bunnyFollow != null)
        {
            bunnyFollow.SetTarget(playerInstance.transform);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
