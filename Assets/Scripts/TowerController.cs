using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
public class TowerController : MonoBehaviour
{
    private float _rotateSpeed = 2.6f;
    
    [SerializeField] private PlatformController[] platforms;
    
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
        for (var i = 1; i < floorNumbers * 1/3; i++)
        {
            _floors[i] = new PlatformController[360 / platforms[0].angle];

            for (int angle = platforms[0].angle; angle <= 360;
                angle += platforms[0].angle)
            {
                PlatformController p = Instantiate(platforms[0], 
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
                _floors[i][(angle / platforms[0].angle) - 1] = p;

                var k = Random.Range(0, (angle / platforms[0].angle) - 1);
                _floors[i][k].GetComponent<MeshRenderer>().material = undestroyable;
                _floors[i][k].gameObject.tag = "Undestroyable";
            }
        }
        
        for (var i = floorNumbers * 1/3; i < floorNumbers * 2/3; i++)
        {
            _floors[i] = new PlatformController[360 / platforms[1].angle];
        
            for (int angle = platforms[1].angle; angle <= 360;
                angle += platforms[1].angle)
            {
                PlatformController p = Instantiate(platforms[1], 
                    transform.position + new Vector3(0, (_platformHeight + _platformGap) * i, 0),
                    Quaternion.Euler(0, angle + _angleTurn * i, 0));
                p.transform.parent = this.transform;
                
                _floors[i][(angle / platforms[1].angle) - 1] = p;
        
                var k = Random.Range(0, (angle / platforms[1].angle) - 1);
                _floors[i][k].GetComponent<MeshRenderer>().material = undestroyable;
                _floors[i][k].gameObject.tag = "Undestroyable";
            }
        }
        
        for (var i = floorNumbers * 2/3; i < floorNumbers; i++)
        {
            _floors[i] = new PlatformController[360 / platforms[2].angle];
        
            for (int angle = platforms[2].angle; angle <= 360;
                angle += platforms[2].angle)
            {
                PlatformController p = Instantiate(platforms[2], 
                    transform.position + new Vector3(0, (_platformHeight + _platformGap) * i, 0),
                    Quaternion.Euler(0, angle + _angleTurn * i, 0));
                p.transform.parent = this.transform;
               
                _floors[i][(angle / platforms[2].angle) - 1] = p;
        
                var k = Random.Range(0, (angle / platforms[2].angle) - 1);
                _floors[i][k].GetComponent<MeshRenderer>().material = undestroyable;
                _floors[i][k].gameObject.tag = "Undestroyable";
            }
        }
    }
    public void PopFloors()
    {
        try
        {
            for (var j = 0; j < (360 / platforms[Random.Range(0, platforms.Length)].angle); j++)
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
