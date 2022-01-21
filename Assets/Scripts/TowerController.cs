using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    [SerializeField] private GameObject[] rings;
    [SerializeField] private float ringsDistance;
    [SerializeField] private int ringsNum = 0;
    void Start()
    {
        for (int i = 0; i < ringsNum; i++)
        {
            NewRings(Random.Range(0, rings.Length-1));
        }
        NewRings(rings.Length - 1);
    }
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.smoothDeltaTime, 0);
    }
    private void NewRings(int ringIndex)
    {
        GameObject ring = Instantiate(rings[ringIndex], transform.up * ringsDistance,
            Quaternion.AngleAxis(Random.Range(0f,360f), Vector3.up));
        ring.transform.parent = transform;
        ringsDistance -= 3f;
    }
}
