using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cube;
    public GameObject peon;
    private GameObject[] cubeVectorStorage;
    private GameObject[,] cube2dVectorStorage;
    private int[,] virtualBoard;
    PeonData peonData;
    CubeData cubeData;

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
        GameObject cubeTemp;
        cube2dVectorStorage = new GameObject[numCasas, numCasas];
        cubeVectorStorage = new GameObject[numCasas * numCasas];
        virtualBoard = new int[numCasas, numCasas];
        int a = 0;

        for (int i = 0; i < numCasas; ++i)
        {
            Debug.Log("ola");
            for (int j = 0; j < numCasas; ++j)
            {      
                Vector3 spawnCubeLoc = new Vector3 (j,0,i) + Vector3.zero;
                cubeTemp = Instantiate(cube, spawnCubeLoc, Quaternion.identity,this.transform);
                var cubeData = cubeTemp.GetComponent<CubeData>();
                cubeTemp.name = i + "." + j;
                cubeData.CuboCordenada[0, 0] = i;
                cubeData.CuboCordenada[1, 0] = j;
                cubeVectorStorage[a] = cubeTemp;
                a++;
                cube2dVectorStorage[i,j] = cubeTemp;       
            }
        }
        SpawnBoardObjects(numCasas);
    }
    public void SpawnBoardObjects(int numCasas)
    {

        Vector3 posCubo = new Vector3(0, 0.90f, 0);

        virtualBoard[0, numCasas/2] = 1;

        for (int i = 0; i <= numCasas - 1; ++i)
        {
            for (int j = 0; j <= numCasas - 1; ++j)
            {
                switch (virtualBoard[i, j])
                {
                    case 1:

                        posCubo += cube2dVectorStorage[i, j].transform.position;
                        var peaoCriado = Instantiate(peon, posCubo, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        peonData = peaoCriado.GetComponent<PeonData>();

                        //peao

                        //peonData.CreateStatus(3,3,2,2,i,j);
                        peonData.Health = 3;
                        peonData.Move = 3;
                        peonData.Damage = 2;
                        peonData.AtkRange = 2;
                        peonData.PosicaoPeao[0, 0] = i;
                        peonData.PosicaoPeao[1, 0] = j;
                        peonData.Player = 1;
                        //peonData.ReceiveDamage();

                        //cubo

                        cube2dVectorStorage[i, j].GetComponent<CubeData>().CuboFillId = 1;

                        break;
                    default:

                        break;
                }
            }
        }
    }
}
