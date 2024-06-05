using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform playerTransform; // Reference to the player's transform

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

    public void RegisterBunny(GameObject bunny)
    {
        bnuuyFollow bunnyFollow = bunny.GetComponent<bnuuyFollow>();
        if (bunnyFollow != null)
        {
            bunnyFollow.SetTarget(playerTransform);
        }
    }
}
