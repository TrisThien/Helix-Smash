using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameOver;
    public GameObject losePanel;

    void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }
}