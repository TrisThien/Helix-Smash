using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int angle;
    private bool _popped;
    private const float Speed = 16f;
    [SerializeField] private new MeshCollider collider;
    private Vector3 _firstPos;
    private void Start()
    {
        _popped = false;
        _firstPos = transform.position;
    }
    private void Update()
    {
        if (_popped)
        {
            collider.enabled = false;
            transform.parent = null;
            transform.position = Vector3.MoveTowards(transform.position, transform.position +
                (transform.rotation.eulerAngles.y > 180 ? Vector3.right : Vector3.left) + transform.up, Time.deltaTime * Speed);
            
            if (Vector3.Distance(transform.position, _firstPos) >= 10f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Pop()
    {
        _popped = true;
    }
}