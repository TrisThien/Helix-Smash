using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text level;
    public static int Levelnum = 1;
    public static bool GameOver;
    public static bool YouWin;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    private void Start()
    {
        GameOver = false;
        YouWin = false;
    }

    private void Update()
    {
        level.text = "LEVEL: " + Levelnum.ToString();

        if (GameOver)
        {
            Time.timeScale = 0;
            levelPanel.SetActive(false);
            losePanel.SetActive(true);
        }
        if (YouWin)
        {
            levelPanel.SetActive(false);
            winPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextGame()
    {
        Levelnum++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}