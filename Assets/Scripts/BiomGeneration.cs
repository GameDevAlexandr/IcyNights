using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomGeneration : MonoBehaviour
{
    [System.Serializable]
    public struct envirParam
    {
        public GameObject envirObject;
        [Space]
        public int maxCount;
        public Vector2 scaleVariaton;
        public float noPutRadius;
        public LayerMask ObjectLayer;
    }
    [SerializeField] envirParam[] envirParams;
    public Vector2 sizeSurface;
    [SerializeField] private LayerMask surfaceLayer;
    [SerializeField] private float depth;
    private List<GameObject> envirObjects = new List<GameObject>();
    private void Awake()
    {
      
    }

    private void DropElement(int elementIndex)
    {
        float posX = transform.position.x + Random.Range(-sizeSurface.x/2, sizeSurface.x/2);
        float posZ = transform.position.z + Random.Range(-sizeSurface.y/2, sizeSurface.y/2);
        Vector3 rayPosition = new Vector3(posX, transform.position.y, posZ);
        RaycastHit _hit;
        Ray _ray = new Ray(rayPosition, Vector3.down);
        if(Physics.Raycast(_ray, out _hit, depth, surfaceLayer))
        {
            if (!Physics.CheckSphere(_hit.point, envirParams[elementIndex].noPutRadius, envirParams[elementIndex].ObjectLayer))
            {
                GameObject newObject = Instantiate(envirParams[elementIndex].envirObject, _hit.point, Quaternion.identity);
                newObject.transform.Rotate(Vector3.down, Random.Range(0, 360));
                newObject.transform.localScale *= Random.Range(envirParams[elementIndex].scaleVariaton.x, 
                    envirParams[elementIndex].scaleVariaton.y);
                envirObjects.Add(newObject);
                AddObjectToCell(newObject);
               // newObject.SetActive(false);
            }
        }      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearEnvironment();
            GenerateEnviron();
        }
    }
    public void GenerateEnviron()
    {
        for (int i = 0; i < envirParams.Length; i++)
        {
            for (int j = 0; j < envirParams[i].maxCount; j++)
            {
                DropElement(i);
            }                       
        }
    }
    private void ClearEnvironment()
    {
        if (envirObjects.Count != 0)
        {
            for (int i = 0; i < envirObjects.Count; i++)
            {
                Destroy(envirObjects[i]);
            }
            envirObjects.Clear();
        }
    }
    private void AddObjectToCell(GameObject gObject)
    {
        Vector2 objectPosition = new Vector2(gObject.transform.position.x, gObject.transform.position.z);
        Vector2[,] gridSurface = GameData.DataSingleton.gridOfSurface;
        for (int i = 0; i < GameData.DataSingleton.cellSurfaceX-1; i++)
        {
            for (int j = 0; j < GameData.DataSingleton.cellSurfaceY-1; j++)
            {
                if (objectPosition.x > gridSurface[i, j].x && objectPosition.y > gridSurface[i, j].y &&
                    objectPosition.x < gridSurface[i + 1, j + 1].x && objectPosition.y < gridSurface[i + 1, j + 1].y)
                {
                    GameData.DataSingleton.environData[i, j].environObjects.Add(gObject);
                }                
            }
        }
    }
}
