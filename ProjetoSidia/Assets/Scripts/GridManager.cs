using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGrid(int numCasas)
    {
        GameObject cubo;

        for (int i = 0; i < numCasas; ++i)
        {
            Debug.Log("ola");
            for (int j = 0; j < numCasas; ++j)
            {      
                Vector3 spawnCubeLoc = new Vector3 (j,0,i) + Vector3.zero;
                cubo = Instantiate(cube, spawnCubeLoc, Quaternion.identity);
                var cubeData = cubo.GetComponent<CubeData>();
                cubo.name = i + "." + j;
                cubeData.CuboCordenada[0, 0] = i;
                cubeData.CuboCordenada[0, 1] = j;
            }
        }
    }
}
