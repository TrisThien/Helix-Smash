using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text level;
    //private int _levelNum = 1;
    public static bool LoseGame;
    public static bool WinGame;
    public static bool FurryMode;
    public GameObject levelPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject furryPanel;

    [SerializeField] private Image furryCircle;
    public static float FurryImageFill;
    private void Start()
    {
        LoseGame = false;
        WinGame = false;
        FurryMode = false;

        FurryImageFill = 0f;
    }

    private void Update()
    {
        level.text = "LEVEL: " + (SceneManager.GetActiveScene().buildIndex+1);
        
        if (FurryMode)
        {
            furryPanel.SetActive(FurryImageFill > 0);
            furryCircle.fillAmount = BallController.RingCount / 100f;
            FurryImageFill = furryCircle.fillAmount;
        }

        if (LoseGame)
        {
            levelPanel.SetActive(false);
            losePanel.SetActive(true);
            furryPanel.SetActive(false);
        }
        if (WinGame)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}