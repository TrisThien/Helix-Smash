using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text level;
    private int _levelNum = 1;
    public static bool LoseGame;
    public static bool WinGame;
    public static bool FurryMode;
    public GameObject levelPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject furryPanel;

    [SerializeField] private Image furryCircle;
    public static float FurryImageFill;

    // enum GameStates
    // {
    //     Idle,
    //     Smash,
    //     Furry,
    //     Win,
    //     Lose
    // }
    //
    // private GameStates _currentGameState = GameStates.Idle;
    
    private void Start()
    {
        LoseGame = false;
        WinGame = false;
        FurryMode = false;

        FurryImageFill = 0f;
    }

    private void Update()
    {
        level.text = "LEVEL: " + _levelNum.ToString();

        // switch (_currentGameState)
        // {
        //     case GameStates.Idle:
        //         break;
        //     case GameStates.Smash:
        //         break;
        //     case GameStates.Furry:
        //         break;
        //     case GameStates.Win:
        //         break;
        //     case GameStates.Lose:
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
        
        
        
        if (FurryMode)
        {
            furryPanel.SetActive(FurryImageFill > 0);
            furryCircle.fillAmount = BallController.RingCount / 50f;
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

    // private void ChangeState(GameStates newState)
    // {
    //     if (newState == _currentGameState) return;
    //     ExitCurrentState();
    //     _currentGameState = newState;
    //     EnterNewState();
    // }
    
    // private void EnterNewState()
    // {
    //     switch (_currentGameState)
    //     {
    //         case GameStates.Idle:
    //             break;
    //         case GameStates.Smash:
    //             break;
    //         case GameStates.Furry:
    //             break;
    //         case GameStates.Win:
    //             break;
    //         case GameStates.Lose:
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }
    //
    // private void ExitCurrentState()
    // {
    //     switch (_currentGameState)
    //     {
    //         case GameStates.Idle:
    //             break;
    //         case GameStates.Smash:
    //             break;
    //         case GameStates.Furry:
    //             break;
    //         case GameStates.Win:
    //             break;
    //         case GameStates.Lose:
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }

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