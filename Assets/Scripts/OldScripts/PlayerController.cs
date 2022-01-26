// using UnityEngine;
// using Random = UnityEngine.Random;
//
// public class PlayerController : MonoBehaviour
// {
//     #region MyRegion
//
//     // public Rigidbody playerRigid;
//     // private void OnCollisionEnter(Collision other)
//     // {
//     //     playerRigid.velocity = new Vector3(playerRigid.velocity.x, 6, playerRigid.velocity.z);
//     //     
//     //     if (Input.GetMouseButton(0))
//     //     {
//     //         playerRigid.AddForce(new Vector3(0,-1000,0));
//     //
//     //         if (other.gameObject.tag == "Destroyable")
//     //         {
//     //             
//     //         }
//     //         else if (other.gameObject.tag == "Undestroyable")
//     //         {
//     //             GameController.gameOver = true;
//     //         }
//     //         else if (other.gameObject.tag == "LastRing")
//     //         {
//     //             GameController.youWin = true;
//     //         }
//     //     }
//     // }
//
//     #endregion
//     
//     public static PlayerController Instance;
//     public Rigidbody rigidBody;
//     private float _bounceSpeed = 250f;
//     private float _diameter;
//     private Vector3 _lastPosition;
//     private float _stretchAndSquashFactor = 2f;
//
//     private float _moveSpeed = 50f;
//     public enum States
//     {
//         Idle,
//         Smash,
//         Dead,
//         Over
//     }
//     public States currentState = States.Idle;
//
//     private void Awake()
//     {
//         if (Instance == null) Instance = this;
//     }
//
//     private void Start()
//     {
//         if (Time.timeScale == 0)
//         {
//             Time.timeScale = 1;
//         }
//         gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
//         _diameter = transform.localScale.x;
//         _lastPosition = transform.position;
//     }
//
//     private void Update()
//     {
//         switch (currentState)
//         {
//             case States.Idle:
//                 Idle();
//                 break;
//             case States.Smash:
//                 Smash();
//                 break;
//             case States.Dead:
//                 Dead();
//                 break;
//             case States.Over:
//                 Over();
//                 break;
//         }
//
//         float yscale = _diameter + (transform.position.y - _lastPosition.y) * _stretchAndSquashFactor;
//         transform.localScale = new Vector3(transform.localScale.x, yscale, transform.localScale.z);
//
//         _lastPosition = transform.position;
//     }
//     private void Idle()
//     {
//         if (Input.GetMouseButton(0))
//         {
//             currentState = States.Smash;
//         };
//     }
//     private void Smash()
//     {
//         if (!Input.GetMouseButton(0))
//         {
//             currentState = States.Idle;
//         }
//     }
//     
//     private void Dead()
//     {
//         GameController.GameOver = true;
//     }
//     
//     private void Over()
//     {
//         GameController.YouWin = true;
//     }
//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.gameObject.CompareTag("Pie") && currentState == States.Idle)
//         {
//             var velocity = rigidBody.velocity;
//             velocity = new Vector3(velocity.x, _bounceSpeed * Time.deltaTime, velocity.z);
//             rigidBody.velocity = velocity;
//         }
//         if (other.gameObject.CompareTag("Pie") && currentState == States.Smash)
//         {
//             //rigidBody.AddRelativeForce(0, -_moveSpeed, 0);
//             rigidBody.AddRelativeForce(Vector3.down * _moveSpeed * Time.fixedTime);
//         }
//         if (other.gameObject.CompareTag("LastRing"))
//         {
//             currentState = States.Over;
//         }
//     }
// }