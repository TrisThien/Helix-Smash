using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float jumpSpeed = 7.0f;
    [SerializeField] private TowerController tower;
    [SerializeField] private bool isPlayed;
    [SerializeField] private int ringCount = 0;
    
    public enum States
    {
        Idle,
        Smash,
        Dead,
        Over
    }
    public States currentState = States.Idle;
    
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    private void Update()
    {
        switch (currentState)
        {
            case States.Idle:
                Idle();
                break;
            case States.Smash:
                Smash();
                break;
            case States.Dead:
                Dead();
                break;
            case States.Over:
                Over();
                break;
        }
    }

    private void Idle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentState = States.Smash;
            isPlayed = true;
            rigidbody.velocity = Vector3.down * jumpSpeed;
        }
    }
    
    private void Smash()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentState = States.Idle;
            isPlayed = false;
        }
    }
    
    private void Dead()
    {
        throw new System.NotImplementedException();
    }
    
    private void Over()
    {
        GameController.YouWin = true;
    }

    private void FixedUpdate() {
        if (isPlayed)
        {
            if (rigidbody.velocity.magnitude > 7)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * 7;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f))
            {
                if (hit.collider.gameObject.CompareTag("Destroyable"))
                {
                    Physics.IgnoreCollision(hit.collider, GetComponent<Collider>());
                    PopFloor();
                }
            }
        }
    }
    private void PopFloor()
    {
        tower.PopFloors();
        ringCount++;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Jump();
        if (collision.gameObject.CompareTag("LastRing"))
        {
            currentState = States.Over;
        }
    }
    private void Jump()
    {
        ringCount = 0;
        rigidbody.velocity = Vector3.up * jumpSpeed;
    }
}
