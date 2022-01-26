using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject lastBase;
    public float heightGap = 1f;
    private Vector3 _lastPos;
    private void Start()
    {
        _lastPos = transform.position;
    }
    private void Update()
    {
        var pos = transform.position;
        
        if (!(pos.y - heightGap >= ball.transform.position.y) || 
            !(pos.y - heightGap * 4 >= lastBase.transform.position.y)) return;
        var target = new Vector3(pos.x, ball.transform.position.y + heightGap, pos.z);
        pos = Vector3.Lerp(_lastPos, target, Time.time * 0.5f);
        transform.position = pos;
    }
}
