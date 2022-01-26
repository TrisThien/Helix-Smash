using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    private const float JumpSpeed = 7f;
    [SerializeField] private TowerController tower;
    public static float RingCount;
    
    [SerializeField] private ParticleSystem furryEffect;
    
    private enum States
    {
        Idle,
        Smash,
        Lose,
        Win
    }
    private States _currentState = States.Idle;
    
    private void Start()
    {
        furryEffect.Stop();
        RingCount = 0f;
        gameObject.GetComponent<MeshRenderer>().material.color =
            Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case States.Idle:
                Idle();
                if (RingCount > 0)
                {
                    RingCount--;
                }
                break;
            case States.Smash:
                Smash();
                RingCount++;
                break;
            case States.Lose:
                Lose();
                break;
            case States.Win:
                Win();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void Idle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentState = States.Smash;
            rigidbody.velocity = Vector3.down * JumpSpeed;
            GameController.FurryMode = true;
            if (GameController.FurryImageFill >= 1f)
            {
                furryEffect.Play();
                GameController.FurryMode = true;
            }
            else if (GameController.FurryImageFill <= 0)
            {
                furryEffect.Stop();
            }
        }
    }
    private void Smash()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _currentState = States.Idle;
        }

        if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 0.5f)) return;
        if (hit.collider.gameObject.CompareTag("Destroyable"))
        {
            Physics.IgnoreCollision(hit.collider, GetComponent<Collider>());
            tower.PopFloors();
        }
        else if (hit.collider.gameObject.CompareTag("Undestroyable"))
        {
            _currentState = States.Lose;
        }
        else if (hit.collider.gameObject.CompareTag("LastRing"))
        {
            _currentState = States.Win;
        }
        
        if (GameController.FurryMode == true && hit.collider.gameObject.CompareTag("Destroyable") && 
                                       hit.collider.gameObject.CompareTag("Undestroyable"))
        {
            Physics.IgnoreCollision(hit.collider, GetComponent<Collider>());
            tower.PopFloors();
        }
    }
    private void Lose()
    {
        GameController.GameOver = true;
    }
    private void Win()
    {
        GameController.YouWin = true;
        if (furryEffect.isPlaying)
        {
            furryEffect.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = Vector3.up * JumpSpeed;
        Physics.gravity = new Vector3(0, -25f, 0);
    }
}
