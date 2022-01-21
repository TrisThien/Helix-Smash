using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddForceController : MonoBehaviour
{
    #region MyRegion

    // public GameObject broken;
    // private bool isBroken;
    // // public float breakforce;
    //
    // private void Start()
    // {
    //     isBroken = false;
    // }
    //
    // private void Update()
    // {
    //     if (isBroken)
    //     {
    //         GameObject b = Instantiate(broken, transform.position, transform.rotation);
    //         
    //
    //         foreach (Rigidbody rb in b.GetComponentsInChildren<Rigidbody>())
    //         {
    //             // Vector3 force = (rb.transform.position - transform.position).normalized * breakforce;
    //             rb.AddForce(transform.forward * Random.Range(500, 1000));
    //         }
    //         
    //         Destroy(gameObject);
    //     }
    // }
    //
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         isBroken = true;
    //     }
    // }

    #endregion
    public Rigidbody[] rb;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            foreach (Rigidbody r in rb)
            {
                r.AddForce(transform.forward * Random.Range(1000, 2000));
                r.useGravity = true;
                transform.parent.SetParent(null);
                Destroy(transform.parent.gameObject, 2.0f);
            }
        }
    }
}