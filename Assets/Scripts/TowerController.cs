using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class TowerController : MonoBehaviour
{
    private float _rotateSpeed = 2.6f;
    
    [SerializeField] private PlatformController[] platform;
    
    private float _platformHeight = 0.4f;
    private float _platformGap = 0.2f;
    private float _angleTurn = 10f;
    
    [SerializeField] private Material destroyable;
    [SerializeField] private Material undestroyable;
    
    public int floorNumbers;
    private int _lastFloor;
    
    private PlatformController[][] _floors;
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = 
            Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    
        _floors = new PlatformController[floorNumbers][];
        _lastFloor = floorNumbers - 1;
        GenerateLevel();
    
        destroyable.color = Random.ColorHSV(0.5f, 1f, 1f, 1f, 0.5f, 1f);
        undestroyable.color = Color.black;
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _rotateSpeed);
    }
    private void GenerateLevel()
    {
        for (int i = 1; i < floorNumbers; i++)
        {
            _floors[i] = new PlatformController[360 / platform[Random.Range(0, platform.Length)].angle];
    
            for (int angle = platform[Random.Range(0, platform.Length)].angle; angle <= 360;
                angle += platform[Random.Range(0, platform.Length)].angle)
            {
                PlatformController p = Instantiate(platform[Random.Range(0, platform.Length)], 
                    transform.position + new Vector3(0, (_platformHeight + _platformGap) * i, 0),
                    Quaternion.Euler(0, angle + _angleTurn * i, 0));
                p.transform.parent = this.transform;
                #region material

                // if (platform[Random.Range(0, platform.Length)].angle == angle)
                // {
                //     p.GetComponent<MeshRenderer>().material = destroyable;
                //     p.gameObject.tag = "Destroyable";
                // }
                // else
                // {
                //     p.GetComponent<MeshRenderer>().material = undestroyable;
                //     p.gameObject.tag = "Undestroyable";
                // }

                #endregion
                _floors[i][(angle / platform[Random.Range(0, platform.Length)].angle) - 1] = p;

                var k = Random.Range(0, (angle / platform[Random.Range(0, platform.Length)].angle) - 1);
                _floors[i][k].GetComponent<MeshRenderer>().material = undestroyable;
                _floors[i][k].gameObject.tag = "Undestroyable";
            }
        }
    }
    public void PopFloors()
    {
        try
        {
            for (int j = 0; j < (360 / platform[Random.Range(0, platform.Length)].angle); j++)
            {
                _floors[_lastFloor][j].GetComponent<PlatformController>().Pop();
            }

            _lastFloor--;
        }
        catch (IndexOutOfRangeException)
        {
            return;
        }
        catch (NullReferenceException)
        {
            return;
        }
    }
}
