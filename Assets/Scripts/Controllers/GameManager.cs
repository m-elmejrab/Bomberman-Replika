using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    int score = 0;
    bool gameIsPaused = true;
    bool gameIsInitialized = false;

    private void Start()
    {
        UIManager.instance.UpdateScoreText(score);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && gameIsInitialized)
        {
            if (!gameIsPaused)
            {
                PauseGame();
                gameIsPaused = true;
            }
            else
            {
                ResumeGame();
                gameIsPaused = false;
            }
        }
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        UIManager.instance.UpdateScoreText(score);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameIsInitialized = true;
        SoundManager.instance.PlayLevelMusic();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameIsInitialized = true;
        score = 0;
        UIManager.instance.UpdateScoreText(score);
        UIManager.instance.RestartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.instance.PlayLevelMusic();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        UIManager.instance.PauseGame();
        SoundManager.instance.PauseResumeMusic(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        UIManager.instance.ResumeGame();
        SoundManager.instance.PauseResumeMusic(true);
    }


    public void GameOver(bool hasWon)
    {
        SoundManager.instance.PlayWinOrLose(hasWon);

        if (hasWon)
        {
            UIManager.instance.GameOver(hasWon);
        }
        else
        {
            UIManager.instance.GameOver(hasWon);
        }
        Time.timeScale = 0f;
        gameIsPaused = true;
        gameIsInitialized = false;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsGamePaused()
    {
        return gameIsPaused;
    }
}
