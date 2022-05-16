using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    public List<GameObject> envirObjects = new List<GameObject>();
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
                GameObject newObject = Instantiate(envirParams[elementIndex].envirObject, _hit.point, Quaternion.identity, transform);
                newObject.transform.Rotate(Vector3.down, Random.Range(0, 360));
                newObject.transform.localScale *= Random.Range(envirParams[elementIndex].scaleVariaton.x, 
                envirParams[elementIndex].scaleVariaton.y);
                envirObjects.Add(newObject);
                //newObject.SetActive(false);
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

}
