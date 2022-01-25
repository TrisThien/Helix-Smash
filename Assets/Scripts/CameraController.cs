using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject lastBase;
    public float followHeight = 1;
    private Vector3 _lastPos;
    private void Start()
    {
        _lastPos = transform.position;
    }
    private void Update()
    {
        if (ball.transform.position.y + followHeight < transform.position.y && transform.position.y-4 > lastBase.transform.position.y)
        {
            Vector3 target = new Vector3(transform.position.x, ball.transform.position.y + followHeight, transform.position.z);

            transform.position = Vector3.Lerp(_lastPos, target, Time.time * 0.5f);
        }
    }
}
