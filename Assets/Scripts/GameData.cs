using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData DataSingleton;
    
    public Vector2[,] gridOfSurface;
    public int cellSurfaceX;
    public int cellSurfaceY;
    public struct eData
    {
       public List<GameObject> environObjects;
    }
    public eData[,] environData;
    void Awake()
    {
        if (DataSingleton == null)
        {
            DataSingleton = this;
        }
        else if (DataSingleton == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        environData = new eData[cellSurfaceX, cellSurfaceY];
    }

    public  void CreateGrid(Vector3 leftDown, Vector3 size)
    {
        gridOfSurface = new Vector2[cellSurfaceX, cellSurfaceY];
        float XSizeCell;
        float YSizeCell;
        XSizeCell = size.x / cellSurfaceX;
        YSizeCell = size.z / cellSurfaceY;
        for (int i = 0; i < cellSurfaceX; i++)
        {
            for (int j = 0; j < cellSurfaceY; j++)
            {
                gridOfSurface[i, j] = new Vector2(leftDown.x + i * XSizeCell, leftDown.z + j * YSizeCell);
                environData[i, j].environObjects = new List<GameObject>();
            }
        }       
        
    }
}
