using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigid;
    private void OnCollisionEnter(Collision other)
    {
        playerRigid.velocity = new Vector3(playerRigid.velocity.x, 6, playerRigid.velocity.z);
        
        if (Input.GetMouseButton(0))
        {
            playerRigid.AddForce(new Vector3(0,-1000,0));

            if (other.gameObject.tag == "Destroyable")
            {
                
            }
            else if (other.gameObject.tag == "Undestroyable")
            {
                GameController.gameOver = true;
            }
        }
    }
}