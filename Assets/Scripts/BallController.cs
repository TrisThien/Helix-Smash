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
    private void Update()
    {
        if (GameController.FurryImageFill >= 1f && !furryEffect.isPlaying)
        {
            furryEffect.Play();
        }
        else if (GameController.FurryImageFill <= 0 && furryEffect.isPlaying)
        {
            furryEffect.Stop();
        }
        switch (_currentState)
        {
            case States.Idle:
                Idle();
                break;
            case States.Smash:
                Smash();
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
        Physics.gravity = new Vector3(0, -25f, 0);
        
        if (Input.GetMouseButtonDown(0))
        {
            _currentState = States.Smash;
            rigidbody.velocity = Vector3.down * JumpSpeed;
        }
        
        if (RingCount > 0)
        {
            RingCount--;
        }
    }
    private void Smash()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _currentState = States.Idle;
        }
        
        GameController.FurryMode = true;
        RingCount++;

        if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 0.5f)) return;
        if (hit.collider.gameObject.CompareTag("Destroyable") || GameController.FurryImageFill >= 1f)
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
    }
    private void Lose()
    {
        if (furryEffect.isPlaying)
        {
            furryEffect.Stop();
        }
        GameController.GameOver = true;
    }
    private void Win()
    {
        if (furryEffect.isPlaying)
        {
            furryEffect.Stop();
        }
        GameController.YouWin = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity = Vector3.up * JumpSpeed;
    }
}
