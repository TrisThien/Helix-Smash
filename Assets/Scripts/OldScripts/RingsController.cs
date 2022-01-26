// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class RingsController : MonoBehaviour
// {
//     [SerializeField] private bool isDestroyable;
//     private int _notDestroyableCount = 0;
//     [SerializeField] private Rigidbody[] rb;
//     private float _forceAdded = 700f;
//     private void OnCollisionEnter(Collision col)
//     {
//         if (col.gameObject.CompareTag("Player") && PlayerController.Instance.currentState == PlayerController.States.Smash)
//         {
//             if (isDestroyable)
//             {
//                 foreach (Rigidbody r in rb)
//                 {
//                     r.isKinematic = false;
//
//                     r.AddRelativeForce(new Vector3(_forceAdded,_forceAdded,_forceAdded));
//                     r.AddTorque(new Vector3(_forceAdded,_forceAdded,_forceAdded));
//                 
//                     var o = r.gameObject;
//                     o.transform.parent = null;
//                     Destroy(o, 1.0f);
//                 }    
//             }
//             else
//             {
//                 _notDestroyableCount++;
//                 if (_notDestroyableCount >= 2)
//                 {
//                     GameController.GameOver = true;
//                 }
//             }
//             
//         }
//     }
// }
