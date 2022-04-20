using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBiom : MonoBehaviour
{
    [SerializeField] Collider surfaceCollider;
    private List<GameObject> biomOjects = new List<GameObject>();
    
    [System.Serializable]
    public struct biomParam
    {
        public BiomGeneration biom;
        public int biomCount;
    }
    [SerializeField] private int cellsCountX;
    [SerializeField] private int cellsCountY;
    [SerializeField] biomParam[] bioms;
    private List<GameObject> environmentObjects;
    public void Generate()
    {
        ClearEnvironment();
        Vector3 rightTop;
        Vector3 leftDown;
        Vector3 size;
        rightTop = surfaceCollider.bounds.max;
        leftDown = surfaceCollider.bounds.min;
        size = surfaceCollider.bounds.size;
        //GameData.DataSingleton.CreateGrid(leftDown, size);
        for (int i = 0; i < bioms.Length; i++)
        {
            for (int j = 0; j < bioms[i].biomCount; j++)
            {
                Vector3 biomPosition = new Vector3(Random.Range(leftDown.x, rightTop.x), transform.position.y,
                Random.Range(leftDown.z, rightTop.z));
                GameObject newBiom = Instantiate(bioms[i].biom.gameObject, biomPosition, Quaternion.identity);
                newBiom.GetComponent<BiomGeneration>().GenerateEnviron();
                biomOjects.Add(newBiom);
            }

        }
    }
    public void ClearEnvironment()
    {
        if (biomOjects.Count != 0)
        {
            for (int i = 0; i < biomOjects.Count; i++)
            {
                DestroyImmediate(biomOjects[i], true);
            }
            biomOjects.Clear();
        }
    }
}
