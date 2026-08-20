using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        if (AudioManager.Instance != null) AudioManager.Instance.PlayLoseSound();
    }

    public void ShowWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        if (AudioManager.Instance != null) AudioManager.Instance.PlayWinSound();
    }
}