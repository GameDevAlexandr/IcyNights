using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBiom : MonoBehaviour
{
    [SerializeField] Collider surfaceCollider;
    [SerializeField]private List<BiomGeneration> biomOjects = new List<BiomGeneration>();
    
    [System.Serializable]
    public struct biomParam
    {
        public BiomGeneration biom;
        public int biomCount;
    }

    [System.Serializable]
    public struct eData
    {
        public List<GameObject> environObjects;
    }
    public eData[,] environData;

    public int cellSurfaceX;
    public int cellSurfaceY;
    [SerializeField] biomParam[] bioms;
    public Vector2[,] gridOfSurface;
    private Vector3 rightTop;
    private Vector3 leftDown;
    private Vector3 size;

    private void Awake()
    {
        environData = new eData[cellSurfaceX + 1, cellSurfaceY + 1];
        CreateGrid();
        Debug.Log(biomOjects.Count);
        for (int i = 0; i < biomOjects.Count; i++)
        {
            for (int j = 0; j < biomOjects[i].envirObjects.Count; j++)
            {
               AddObjectToCell(biomOjects[i].envirObjects[j]);
            }
        }
        for (int i = 0; i < cellSurfaceX; i++)
        {
            for (int j= 0; j < cellSurfaceY; j++)
            {
                Debug.Log(environData[i, j].environObjects.Count);
            }
        }
        
        HideAll();
    }
    private void Start()
    {
       
    }
    public void Generate()
    {
        ClearEnvironment();
        GameObject bufer = new GameObject("Bioms");
        
        rightTop = surfaceCollider.bounds.max;
        leftDown = surfaceCollider.bounds.min;
        size = surfaceCollider.bounds.size;
        for (int i = 0; i < bioms.Length; i++)
        {
            for (int j = 0; j < bioms[i].biomCount; j++)
            {
                Vector3 biomPosition = new Vector3(Random.Range(leftDown.x, rightTop.x), transform.position.y,
                Random.Range(leftDown.z, rightTop.z));
                GameObject newBiom = Instantiate(bioms[i].biom.gameObject, biomPosition, Quaternion.identity);
                newBiom.transform.parent = bufer.transform;
                BiomGeneration bg = newBiom.GetComponent<BiomGeneration>();
                bg.GenerateEnviron();
                biomOjects.Add(bg);
            }

        }
        
    }
    public void ClearEnvironment()
    {
        biomOjects.Clear();
        DestroyImmediate(GameObject.Find("Bioms"));
    }
    public void AddObjectToCell(GameObject gObject)
    {
        Vector2 objectPosition = new Vector2(gObject.transform.position.x, gObject.transform.position.z);
        for (int i = 0; i < cellSurfaceX; i++)
        {
            for (int j = 0; j < cellSurfaceY; j++)
            {
                if (objectPosition.x > gridOfSurface[i, j].x && objectPosition.y > gridOfSurface[i, j].y &&
                    objectPosition.x < gridOfSurface[i + 1, j + 1].x && objectPosition.y < gridOfSurface[i + 1, j + 1].y)
                {
                    environData[i, j].environObjects.Add(gObject);
                }
            }
        }
    }
    public void CreateGrid()
    {
        gridOfSurface = new Vector2[cellSurfaceX+1, cellSurfaceY+1];
        float XSizeCell;
        float YSizeCell;
        XSizeCell = size.x / cellSurfaceX;
        YSizeCell = size.z / cellSurfaceY;
        for (int i = 0; i < cellSurfaceX+1; i++)
        {
            for (int j = 0; j < cellSurfaceY+1; j++)
            {
                gridOfSurface[i, j] = new Vector2(leftDown.x + i * XSizeCell, leftDown.z + j * YSizeCell);
                environData[i, j].environObjects = new List<GameObject>();
                //GameObject sh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //sh.transform.position = new Vector3(gridOfSurface[i, j].x, 0, gridOfSurface[i, j].y);
            }
        }
    }
    public void HideAll()
    {
        for (int i = 0; i < cellSurfaceX+1; i++)
        {
            for (int j = 0; j < cellSurfaceY+1; j++)
            {
                for (int k = 0; k < environData[i, j].environObjects.Count; k++)
                {
                    if (environData[i, j].environObjects[k])
                    {
                       environData[i, j].environObjects[k].SetActive(false);
                    }
                }
            }
        }
    }
}
