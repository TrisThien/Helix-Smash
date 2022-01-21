using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region MyRegion

    // public Rigidbody playerRigid;
    // private void OnCollisionEnter(Collision other)
    // {
    //     playerRigid.velocity = new Vector3(playerRigid.velocity.x, 6, playerRigid.velocity.z);
    //     
    //     if (Input.GetMouseButton(0))
    //     {
    //         playerRigid.AddForce(new Vector3(0,-1000,0));
    //
    //         if (other.gameObject.tag == "Destroyable")
    //         {
    //             
    //         }
    //         else if (other.gameObject.tag == "Undestroyable")
    //         {
    //             GameController.gameOver = true;
    //         }
    //         else if (other.gameObject.tag == "LastRing")
    //         {
    //             GameController.youWin = true;
    //         }
    //     }
    // }

    #endregion
    public Rigidbody rigidBody;
    [SerializeField] private float speed;
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.smoothDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 6, rigidBody.velocity.z);
        if (other.gameObject.CompareTag("LastRing"))
        {
            GameController.youWin = true;
        }
    }
}