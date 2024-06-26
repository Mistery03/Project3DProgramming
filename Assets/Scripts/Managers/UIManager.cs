using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject deathPanel;
    public Text hpText;

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

    private void Start()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false); // Ensure the death panel is hidden at the start
        }
    }

    public void UpdateHP(float currentHP, float maxHP)
    {
        hpText.text = "HP: " + Mathf.Floor(currentHP) + "/" + Mathf.Floor(maxHP);
    }

    public void ShowDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("LabArea");
    }

    
}
