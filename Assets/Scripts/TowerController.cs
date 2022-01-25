using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class TowerController : MonoBehaviour
{
    private float _rotateSpeed = 2.0f;
    //[SerializeField] private GameObject platform;
    //private PlatformController _platformController;
    
    [SerializeField] private PlatformController platform;

    private float _platformHeight = 0.4f;
    private float _platformGap = 0.1f;
    private float _angleTurn = 6.0f;

    [SerializeField] private Material destroyable;
    [SerializeField] private Material undestroyable;

    public int floorNumbers = 30;
    private int _lastFloor;

    private PlatformController[][] _floors;
    private void Start()
    {
        //_platformController = platform.GetComponent<PlatformController>();
        
        _floors = new PlatformController[floorNumbers][];
        _lastFloor = floorNumbers-1;
        GenerateLevel();

        destroyable.color = Random.ColorHSV(0.5f, 1f, 1f, 1f, 0.5f, 1f);
        undestroyable.color = Random.ColorHSV(0f, 0.5f, 1f, 1f, 0f, 0.5f);
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < floorNumbers; i++)
        {
            _floors[i] = new PlatformController[360/platform.angle];

            for (int angle = platform.angle; angle <= 360; angle += platform.angle)
            {
                PlatformController p = Instantiate(platform, transform.position+new Vector3(0, (_platformHeight + _platformGap) * i, 0),
                    Quaternion.Euler(0, angle + _angleTurn * i, 0));
                p.transform.parent = this.transform;

                if (platform.angle == angle)
                {
                    p.GetComponent<MeshRenderer>().material = destroyable;
                    p.gameObject.tag = "Destroyable";
                }
                else
                {
                    p.GetComponent<MeshRenderer>().material = undestroyable;
                }

                _floors[i][(angle/platform.angle)-1] = p;
            }
        }
    }

    public void PopFloors()
    {
        try
        {
            for (int j = 0; j < 360/platform.angle; j++)
            {
                _floors[_lastFloor][j].GetComponent<PlatformController>().Pop();
            }
            _lastFloor--;
        }
        catch (IndexOutOfRangeException)
        {
            return;
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _rotateSpeed);
    }
}
