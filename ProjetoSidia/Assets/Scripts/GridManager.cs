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
    private int numeroCasas;
    PeonData peonData;
    CubeData cubeData;
    public Camera Maincamera;


    // Start is called before the first frame update
    void Start()
    {
        numeroCasas = GameManager.numeroCasas;
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
        Vector3 camPos = new Vector3 (0,numCasas,0);
          camPos += ((cube2dVectorStorage[0, 0].transform.position + cube2dVectorStorage[numCasas - 1, numCasas - 1].transform.position) / 2);
        Debug.Log(camPos);
        Maincamera.transform.position = camPos;
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

    public void PrepareMov(GameObject peonTemp)
    {
        peonData = peonTemp.GetComponent<PeonData>();
        int movimento = peonData.Move;
        int posPeaoCol = peonData.PosicaoPeao[0, 0];
        int posPeaoLin = peonData.PosicaoPeao[1, 0];
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = false;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        for (int i = -movimento; i <= movimento; ++i)
        {
            for (int j = -movimento; j <= movimento; ++j)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= movimento && (posPeaoCol + i >= 0 && posPeaoLin + j >= 0 && posPeaoCol + i <= numeroCasas -1 && posPeaoLin + j <= numeroCasas -1 ))
                {
                    cube2dVectorStorage[posPeaoCol + i, posPeaoLin + j].GetComponent<BoxCollider>().enabled = true;
                    cube2dVectorStorage[posPeaoCol + i, posPeaoLin + j].GetComponent<Renderer>().material.color = new Color(0, 0, 0.50f, 1);
                }
            }
        }
    }

    public void PrepareAtk(GameObject peonTemp)
    {
        peonData = peonTemp.GetComponent<PeonData>();
        int atkRange = peonData.AtkRange;
        int posPeaoCol = peonData.PosicaoPeao[0, 0];
        int posPeaoLin = peonData.PosicaoPeao[1, 0];
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = false;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        for (int i = -atkRange; i <= atkRange; ++i)
        {
            for (int j = -atkRange; j <= atkRange; ++j)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= atkRange && (posPeaoCol + i >= 0 && posPeaoLin + j >= 0 && posPeaoCol + i <= numeroCasas - 1 && posPeaoLin + j <= numeroCasas - 1))
                {
                    cube2dVectorStorage[posPeaoCol + i, posPeaoLin + j].GetComponent<BoxCollider>().enabled = true;
                    cube2dVectorStorage[posPeaoCol + i, posPeaoLin + j].GetComponent<Renderer>().material.color = new Color(0.5f, 0, 0, 1);
                }
            }
        }
    }
    public void Movimento (GameObject peaoTemp, GameObject cuboTemp)
    {
        StartCoroutine(PeonMoveAnim(peaoTemp, cuboTemp));
    }

    IEnumerator PeonMoveAnim(GameObject peaoTemp, GameObject cuboTemp)
    {
        float elapsedTime = 0;
        float waitTime = 0.5f;
        Vector3 peaoPos = peaoTemp.transform.position;
        Vector3 cuboPos = cuboTemp.transform.position;
        Vector3 up = new Vector3(0, 0.90f, 0);

        while (elapsedTime < waitTime)
        {
            peaoTemp.transform.position = Vector3.Lerp(peaoPos, cuboPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            Debug.Log(peaoTemp.transform.position);
            Debug.Log(elapsedTime);
            yield return null;
        }

    }
}
