using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    private const float JumpSpeed = 7f;
    [SerializeField] private TowerController tower;
    public static int RingCount;
    private int _undestroyableTouch = 1;
    
    [SerializeField] private ParticleSystem furryEffect;
    [SerializeField] private ParticleSystem explodeEffect;
    private enum BallStates
    {
        Idle,
        Smash,
        Furry,
        Lose,
        Win
    }
    private BallStates _currentBallState = BallStates.Idle;
    
    private void Start()
    {
        Application.targetFrameRate = 60;

        Physics.gravity = new Vector3(0, -25f, 0);
        RingCount = 0;
        gameObject.GetComponent<MeshRenderer>().material.color =
            Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    private void Update()
    {
        switch (_currentBallState)
        {
            case BallStates.Idle:
                if (Input.GetMouseButtonDown(0))
                {
                    GameController.FurryMode = true;
                    ChangeState(BallStates.Smash);
                }
                else
                {
                    RingCount--;
                    if (RingCount <= 0)
                    {
                        RingCount = 0;
                    }
                }
                break;
            case BallStates.Smash:
                RingCount++;

                if (Input.GetMouseButtonUp(0))
                {
                    ChangeState(BallStates.Idle);
                }
                if (RingCount >= 100)
                {
                    RingCount = 100;
                    ChangeState(BallStates.Furry); 
                }
                
                rigidbody.velocity = Vector3.down * JumpSpeed;

                if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 0.5f)) return;
                if (!hit.collider.gameObject.CompareTag("Undestroyable") && !hit.collider.gameObject.CompareTag("LastRing"))
                {
                    Physics.IgnoreCollision(hit.collider, GetComponent<Collider>());
                    tower.PopFloors();
                }
                else if (hit.collider.gameObject.CompareTag("Undestroyable"))
                {
                    if (_undestroyableTouch == 1)
                    {
                        var scaleSequence = DOTween.Sequence();
                        scaleSequence.Append(hit.collider.transform.DOScaleZ(0.015f, 0.1f))
                            .Append(hit.collider.transform.DOScaleZ(0.01f, 0.1f));
                        ChangeState(BallStates.Idle);
                        _undestroyableTouch--;
                    }
                    else
                    {
                        ChangeState(BallStates.Lose);
                    }
                }
                else if (hit.collider.gameObject.CompareTag("LastRing"))
                {
                    ChangeState(BallStates.Win);
                }
                break;
            case BallStates.Furry:
                RingCount--;
                
                if (GameController.FurryImageFill >= 1f && !furryEffect.isPlaying)
                {
                    furryEffect.Play();
                }
                else if (GameController.FurryImageFill <= 0 && furryEffect.isPlaying)
                {
                    furryEffect.Stop();
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    ChangeState(BallStates.Idle);
                    if(furryEffect.isPlaying) furryEffect.Stop();
                }
                
                if (GameController.FurryImageFill == 0)
                {
                    ChangeState(BallStates.Smash);
                }
                
                if (!Physics.Raycast(transform.position, Vector3.down, out var furryhit, 0.5f)
                && !Input.GetMouseButtonDown(0)) return;
                Physics.IgnoreCollision(furryhit.collider, GetComponent<Collider>());
                rigidbody.velocity = Vector3.down * JumpSpeed;
                tower.PopFloors();
                if (furryhit.collider.gameObject.CompareTag("LastRing"))
                {
                    ChangeState(BallStates.Smash);
                }
                break;
            case BallStates.Lose:
                GameController.LoseGame = true;
                gameObject.SetActive(false);
                Instantiate(explodeEffect, transform.position, transform.rotation);
                break;
            case BallStates.Win:
                GameController.WinGame = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void ChangeState(BallStates newState)
    {
        if (newState == _currentBallState) return;
        ExitCurrentState();
        _currentBallState = newState;
        EnterNewState();
    }
    private void EnterNewState()
    {
        switch (_currentBallState)
        {
            case BallStates.Idle:
                break;
            case BallStates.Smash:
                break;
            case BallStates.Furry:
                break;
            case BallStates.Win:
                if (furryEffect.isPlaying) furryEffect.Stop();
                break;
            case BallStates.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void ExitCurrentState()
    {
        switch (_currentBallState)
        {
            case BallStates.Idle:
                break;
            case BallStates.Smash:
                break;
            case BallStates.Furry:
                break;
            case BallStates.Win:
                break;
            case BallStates.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = Vector3.up * JumpSpeed;
    }
}
