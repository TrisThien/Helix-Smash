using UnityEngine;

public class DestroyController : MonoBehaviour
{
    private int time = 0;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("va cham");
            time++;
            if (time == 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
