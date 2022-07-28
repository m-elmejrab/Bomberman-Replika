using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    int score = 0;

    private void Start()
    {
        UIManager.instance.UpdateScoreText(score);
        Time.timeScale = 0f;
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        UIManager.instance.UpdateScoreText(score);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        score = 0;
        UIManager.instance.UpdateScoreText(score);
        UIManager.instance.RestartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver(bool hasWon)
    {
        if (hasWon)
        {
            UIManager.instance.GameOver(hasWon);
        }
        else
        {
            UIManager.instance.GameOver(hasWon);
        }
        Time.timeScale = 0f;
    }

    public int GetScore()
    {
        return score;
    }

}
