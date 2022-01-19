using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddForceController : MonoBehaviour
{
    public Rigidbody[] rb;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            foreach (Rigidbody r in rb)
            {
                int x = Random.Range(900, 1000);
                int z = x;
                int y = x;
                r.AddForce(x, y, z);
                // r.AddForce(transform.forward * 1000);
                r.useGravity = true;
            }
            Destroy(this.transform.parent.gameObject, 2f);
        }
    }
}