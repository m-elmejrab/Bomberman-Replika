using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject lossMenu;
    [SerializeField] GameObject winMenu;

    private void Start()
    {
        lossMenu.SetActive(false);
        winMenu.SetActive(false);
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
    }

    public void RestartGame()
    {
        startMenu.SetActive(false);
        lossMenu.SetActive(false);
        winMenu.SetActive(false);
        scoreText.gameObject.SetActive(true);

    }

    public void GameOver(bool hasWon)
    {
        if (hasWon)
        {
            winMenu.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            lossMenu.SetActive(true);
        }
    }
}
