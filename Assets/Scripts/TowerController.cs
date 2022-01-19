using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject[] rings;
    public float ringsDistance;
    public int ringsNum = 0;
    void Start()
    {
        for (int i = 0; i < ringsNum; i++)
        {
            NewRings(Random.Range(0, rings.Length-1));
        }
        NewRings(rings.Length - 1);
    }
    private void NewRings(int ringIndex)
    {
        GameObject ring = Instantiate(rings[ringIndex], transform.up * ringsDistance,
            Quaternion.AngleAxis(Random.Range(0f,360f), Vector3.up));
        ring.transform.parent = transform;
        ringsDistance -= 3f;
    }
}
