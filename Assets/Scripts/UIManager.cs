using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject deathPanel;
    public GameObject pausePanel; // Reference to the PausePanel
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
            deathPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void UpdateHP(float currentHP, float maxHP)
    {
        hpText.text = "HP: " + Mathf.Floor(currentHP) + "/" + Mathf.Floor(maxHP);
    }

    public void setHpText(Text hpText)
    {
        this.hpText = hpText;
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
        Time.timeScale = 1f;
        GameManager.Instance.AudioDestroy();
        SceneManager.LoadScene("LabArea");
    }

    public void PauseGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1f; 
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu or quit the game
    }
}
