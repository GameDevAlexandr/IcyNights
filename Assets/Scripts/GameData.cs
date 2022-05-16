using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData DataSingleton;
    
    public Vector2[,] gridOfSurface;
    public int cellSurfaceX;
    public int cellSurfaceY;
    public ItemsData ItemsData;
  
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
        //environData = new eData[cellSurfaceX, cellSurfaceY];
    }

   
}
