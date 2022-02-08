using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text level;
    public static bool LoseGame;
    public static bool WinGame;
    public static bool FurryMode;
    public GameObject levelPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject furryPanel;

    [SerializeField] private Image furryCircle;
    public static float FurryImageFill;
    private enum GameStates
    {
        Idle, Smash, Win, Lose
    }
    private GameStates _currentGameState = GameStates.Idle;
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
        
        switch (_currentGameState)
        {
            case GameStates.Idle:
                if(FurryMode) ChangeState(GameStates.Smash);
                break;
            case GameStates.Smash:
                furryPanel.SetActive(FurryImageFill > 0);
                furryCircle.fillAmount = BallController.RingCount / 100f;
                FurryImageFill = furryCircle.fillAmount;
                
                if(WinGame) ChangeState(GameStates.Win);
                if(LoseGame) ChangeState(GameStates.Lose);
                break;
            case GameStates.Win:
                levelPanel.SetActive(false);
                winPanel.SetActive(true);
                furryPanel.SetActive(false);
                break;
            case GameStates.Lose:
                levelPanel.SetActive(false);
                losePanel.SetActive(true);
                furryPanel.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void ChangeState(GameStates newState)
    {
        if (newState == _currentGameState) return;
        ExitCurrentState();
        _currentGameState = newState;
        EnterNewState();
    }
    private void EnterNewState()
    {
        switch (_currentGameState)
        {
            case GameStates.Idle:
                break;
            case GameStates.Smash:
                break;
            case GameStates.Win:
                break;
            case GameStates.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void ExitCurrentState()
    {
        switch (_currentGameState)
        {
            case GameStates.Idle:
                break;
            case GameStates.Smash:
                break;
            case GameStates.Win:
                break;
            case GameStates.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
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