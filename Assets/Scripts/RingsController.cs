using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsController : MonoBehaviour
{
    [SerializeField] private bool isDestroyable;
    [SerializeField] private Rigidbody[] rb;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (PlayerController.IsPlayed)
            {
                if (isDestroyable)
                {
                    foreach (Rigidbody r in rb)
                    {
                        r.AddRelativeForce(new Vector3(500,500,500));

                        r.AddTorque(new Vector3(500,500,500));
                        r.velocity = Vector3.down * Time.smoothDeltaTime;

                        Destroy(transform.parent.gameObject, 2.0f);
                    }
                }
                else
                {
                    GameController.gameOver = true;
                }
            }
        }
    }
}
