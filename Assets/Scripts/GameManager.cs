using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public AudioClip newMusicClip; // The new music clip to play when an animal follows the player

    public AudioClip oriMusicClip;

    private GameObject playerInstance;
    private TopDownCamera cameraFollow;
    private Player playerScript;
    private AudioSource audioSource; // The audio source for background music

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes
            audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to the GameManager
            ChangeBackgroundMusic(oriMusicClip);
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
            playerScript = playerInstance.GetComponent<Player>(); // Get the Player script component
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

    public void RegisterBunny(GameObject animal)
    {
        animalFollow animFollow = animal.GetComponent<animalFollow>();
        if (animFollow != null)
        {
            animFollow.SetTarget(playerInstance.transform);
            ChangeBackgroundMusic(newMusicClip); // Change the background music when the animal starts following the player
        }
    }

    public void ChangeScene(string sceneName)
    {
        Destroy(audioSource);
        SceneManager.LoadScene(sceneName);
    }

    public void PlayerTakeDamage(int damage)
    {
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage);
        }
    }

    private void ChangeBackgroundMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
