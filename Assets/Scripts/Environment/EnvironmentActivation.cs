using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentActivation : MonoBehaviour
{
    private int xPositioToGrid;
    private int yPositioToGrid;
    private Vector2[,] gridSurface;
    void Start()
    {
        DefineRect();
        MovmentPlayer.ActivateEnvironEvent.AddListener(CheckRect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnvironActivate()
    {

    }
    private void DefineRect()
    {
        gridSurface = GameData.DataSingleton.gridOfSurface;
        Vector2 objectPosition = new Vector2(transform.position.x, transform.position.z);
        for (int i = 0; i < GameData.DataSingleton.cellSurfaceX; i++)
        {
            for (int j = 0; j < GameData.DataSingleton.cellSurfaceY; j++)
            {
                if (objectPosition.x > gridSurface[i, j].x && objectPosition.y > gridSurface[i, j].y &&
                    objectPosition.x < gridSurface[i + 1, j + 1].x && objectPosition.y < gridSurface[i + 1, j + 1].y)
                {
                    xPositioToGrid = i;
                    yPositioToGrid = j;
                    Debug.Log(transform.position.x + "   " + transform.position.y);
                    Debug.Log(gridSurface[xPositioToGrid+1, yPositioToGrid+1].x + "   " + gridSurface[xPositioToGrid+1, yPositioToGrid+1].y);
                    EnableCell(0, 0);
                    return;
                }
            }
        }
        
    }
    private void CheckRect()
    {
        int xDerect = 0;
        int yDerect = 0;
        if(xPositioToGrid+1<= GameData.DataSingleton.cellSurfaceX && yPositioToGrid + 1 <= GameData.DataSingleton.cellSurfaceY && 
            transform.position.x > gridSurface[xPositioToGrid + 1, yPositioToGrid + 1].x)
        {
            xPositioToGrid++;
            xDerect = 1;
        }
        else if(transform.position.x < gridSurface[xPositioToGrid, yPositioToGrid].x)
        {
            xPositioToGrid--;
            xDerect = -1;
        } 
        else if(xPositioToGrid + 1 <= GameData.DataSingleton.cellSurfaceX && yPositioToGrid + 1 <= GameData.DataSingleton.cellSurfaceY &&
            transform.position.z > gridSurface[xPositioToGrid + 1, yPositioToGrid + 1].y)
        {
            yPositioToGrid++;
            yDerect = 1;
        }
        else if(transform.position.z < gridSurface[xPositioToGrid, yPositioToGrid].y)
        {
            yPositioToGrid--;
            yDerect = -1;
        }
        if (xDerect != 0 || yDerect != 0)
        {
            EnableCell(xDerect, yDerect);
        }
        

    }
    private void EnableCell(int directX, int directY)
    {
        // Debug.Log(transform.position.z + "----" + gridSurface[xPositioToGrid, yPositioToGrid].y + 1 + "----" + (gridSurface[xPositioToGrid, yPositioToGrid].y - 1));
       
        for (int j = -1; j < 2; j++)
        {
            for (int k = -1; k < 2; k++)
            {
                for (int i = 0; i < GameData.DataSingleton.environData[xPositioToGrid + j, yPositioToGrid + k].environObjects.Count; i++)
                {
                    GameData.DataSingleton.environData[xPositioToGrid + j, yPositioToGrid + k].environObjects[i].SetActive(true);
                }
            }
        }

        if (directX != 0 || directY != 0)
        {
            for (int j = -1; j < 2; j++)
            {
                int x = j * directY + 2 * -directX;
                int y = j * directX + 2 * -directY;
                Debug.Log(x+ "  " + y);
                for (int i = 0; i < GameData.DataSingleton.environData[xPositioToGrid + x, yPositioToGrid + y].environObjects.Count; i++)
                {
                    GameData.DataSingleton.environData[xPositioToGrid + x, yPositioToGrid + y].environObjects[i].SetActive(false);
                }
            }
        }

    }
    private bool CheckNOEmpty(int x, int y)
    {
        if(x<= GameData.DataSingleton.cellSurfaceX && y<= GameData.DataSingleton.cellSurfaceY) 
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }
}
