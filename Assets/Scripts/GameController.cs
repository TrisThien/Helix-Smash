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
    }

    private void Update()
    {
        level.text = "LEVEL: " + _levelNum.ToString();

        furryCircle.fillAmount = BallController.RingCount / 50f;
        FurryImageFill = furryCircle.fillAmount;
        furryPanel.SetActive(FurryImageFill > 0);
        if (FurryImageFill >= 1) FurryImageFill--;
        
            if (GameOver)
        {
            if (furryPanel.activeInHierarchy)
            {
                furryPanel.SetActive(false);
            }
            levelPanel.SetActive(false);
            losePanel.SetActive(true);
        }
        if (YouWin)
        {
            if (furryPanel.activeInHierarchy)
            {
                furryPanel.SetActive(false);
            }
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
        _levelNum++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}