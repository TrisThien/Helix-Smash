using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text level;
    private int _levelNum = 1;
    public static bool GameOver;
    public static bool YouWin;
    public static bool FurryMode;
    public GameObject levelPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject furryPanel;

    [SerializeField] private Image furryCircle;
    public static float FurryImageFill;
    private void Start()
    {
        GameOver = false;
        YouWin = false;
        FurryMode = false;

        FurryImageFill = 0f;
    }

    private void Update()
    {
        level.text = "LEVEL: " + _levelNum.ToString();

        if (FurryMode)
        {
            furryPanel.SetActive(FurryImageFill > 0);
            furryCircle.fillAmount = BallController.RingCount / 50f;
            FurryImageFill = furryCircle.fillAmount;
        }

        if (GameOver)
        {
            levelPanel.SetActive(false);
            losePanel.SetActive(true);
            furryPanel.SetActive(false);
        }
        if (YouWin)
        {
            levelPanel.SetActive(false);
            winPanel.SetActive(true);
            furryPanel.SetActive(false);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _levelNum++;
    }
}