using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameOver;
    public static bool youWin;
    public GameObject losePanel;
    public GameObject winPanel;

    void Start()
    {
        gameOver = false;
        youWin = false;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
        if (youWin)
        {
            winPanel.SetActive(true);
        }
    }
}