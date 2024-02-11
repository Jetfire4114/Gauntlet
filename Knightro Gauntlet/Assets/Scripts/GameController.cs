using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject pauseCanvas;
    public GameObject optionsMenuCanvas;
    public GameObject aboutCanvas;
    public GameObject backgroundAudio;

    private PlayerController playerController;

    private bool gameIsPaused = false;
    private bool escapeKeyPaused = false;

    void Start()
    {
        Time.timeScale = 1f;
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
            PauseGame();
        }

        playerController = FindObjectOfType<PlayerController>();
    }

    public void StartGame()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
            UnpauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        if (escapeKeyPaused && pauseCanvas != null)
        {
            pauseCanvas.SetActive(true);
        }

        if (backgroundAudio != null)
        {
            backgroundAudio.SetActive(false);
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        escapeKeyPaused = false;
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }

        if (backgroundAudio != null)
        {
            backgroundAudio.SetActive(true);
        }
    }

    public void ResumeFromRTGButton()
    {
        UnpauseGame();
    }

    public void OpenOptionsMenu()
    {
        mainMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);
    }

    public void EasyMode()
    {
        playerController.moveSpeed = 6f;
        playerController.timerDuration = 500f;
        playerController.currentTime = playerController.timerDuration;

        optionsMenuCanvas.SetActive(false);
        UnpauseGame();
    }

    public void HardMode()
    {
        playerController.moveSpeed = 3f;
        playerController.timerDuration = 60f;
        playerController.currentTime = playerController.timerDuration;

        optionsMenuCanvas.SetActive(false);
        UnpauseGame();
    }

    public void NormalMode()
    {
        playerController.moveSpeed = 4.5f;
        playerController.timerDuration = 90f;
        playerController.currentTime = playerController.timerDuration;

        optionsMenuCanvas.SetActive(false);
        UnpauseGame();
    }

    public void PauseToOptions()
    {
        pauseCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuToAbout()
    {
        mainMenuCanvas.SetActive(false);
        aboutCanvas.SetActive(true);
    }

    public void PauseToAbout()
    {
        pauseCanvas.SetActive(false);
        aboutCanvas.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                escapeKeyPaused = true;
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }
}
