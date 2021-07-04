using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] objetos;
    public GameObject[] peon;
    private GameObject[] cubeVectorStorage;
    private GameObject[,] cube2dVectorStorage;
    private int[,] virtualBoard;
    private int numeroCasas;
    PeonData peonData;
    CubeData cubeData;
    PickupData pickupData;
    public Camera Maincamera;
    public GameManager gameManager;
    public Material[] materiais;


    // Start is called before the first frame update
    void Start()
    {
        numeroCasas = GameManager.numeroCasas;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateGrid(int numCasas,int playerTurn)
    {
        //pegar dados
        GameObject cubeTemp;
        cube2dVectorStorage = new GameObject[numCasas, numCasas];
        cubeVectorStorage = new GameObject[numCasas * numCasas];
        virtualBoard = new int[numCasas, numCasas];
        int a = 0;

        //criar cubos
        for (int i = 0; i < numCasas; ++i)
        {
            for (int j = 0; j < numCasas; ++j)
            {
                Vector3 spawnCubeLoc = new Vector3(j, 0, i) + Vector3.zero;
                cubeTemp = Instantiate(objetos[0], spawnCubeLoc, Quaternion.identity, this.transform);
                var cubeData = cubeTemp.GetComponent<CubeData>();
                cubeTemp.name = i + "." + j;
                cubeData.CuboCordenada[0, 0] = i;
                cubeData.CuboCordenada[1, 0] = j;
                cubeVectorStorage[a] = cubeTemp;
                a++;
                cube2dVectorStorage[i, j] = cubeTemp;
            }
        }

        //camera dinamica
        Vector3 camPos = new Vector3(0, numCasas, 0);
        Maincamera.transform.parent.position = ((cube2dVectorStorage[0, 0].transform.position + cube2dVectorStorage[numCasas - 1, numCasas - 1].transform.position) / 2);
        Maincamera.transform.position += camPos;
        PreparePeonSpawn(numCasas, playerTurn);
        //SpawnBoardObjects(numCasas);
    }

    public void PreparePeonSpawn(int numCasas, int playerTurn)
    {   
        int possibleSlot = numCasas / 3;
        possibleSlot = Mathf.CeilToInt(possibleSlot);

        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = false;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        if (playerTurn == 2)
        {
            for (int i = numCasas -1 ; i >= numCasas - possibleSlot; --i)
            {
                for (int j = 0; j <= numCasas - 1; ++j)
                {
                    cube2dVectorStorage[i, j].GetComponent<BoxCollider>().enabled = true;
                    cube2dVectorStorage[i, j].GetComponent<Renderer>().material.color = new Color(0.25f, 0.50f, 0, 1);  
                }

            }
        }
        else if (playerTurn == 1)
        {
            for (int i = 0; i < possibleSlot; ++i)
            {
                for (int j = 0; j <= numCasas-1; ++j)
                {
                    cube2dVectorStorage[i, j].GetComponent<BoxCollider>().enabled = true;
                    cube2dVectorStorage[i, j].GetComponent<Renderer>().material.color = new Color(0, 0.50f, 0.25f, 1);
                }

            }
        }
    }

    public void SpawnPlayerPeons(GameObject cubeTemp, int playerTurn, int peonType)
    {

        Vector3 posObjeto = new Vector3(0, 0.90f, 0);
        GameObject peaoCriado;

        switch (peonType)
        {
            case 1:

                //criar peao
                posObjeto += cubeTemp.transform.position;
                peaoCriado = Instantiate(peon[0], posObjeto, Quaternion.identity, cubeTemp.transform);
                peaoCriado.GetComponent<MeshRenderer>().material = materiais[0];
                peonData = peaoCriado.GetComponent<PeonData>();
                //dar valor ao peao
                peonData.Health = 3;
                peonData.Move = 3;
                peonData.Damage = 2;
                peonData.AtkRange = 2;
                //peonData.PosicaoPeao[0, 0] = i;
                //peonData.PosicaoPeao[1, 0] = j;
                peonData.Player = 1;
                //cubo
                cubeTemp.GetComponent<CubeData>().updateFill();

                break;
            case 2:

                //criar peao
                posObjeto += cubeTemp.transform.position;
                peaoCriado = Instantiate(peon[0], posObjeto, Quaternion.identity, cubeTemp.transform);
                peaoCriado.GetComponent<MeshRenderer>().material = materiais[1];
                peonData = peaoCriado.GetComponent<PeonData>();
                //dar valor ao peao
                peonData.Health = 3;
                peonData.Move = 3;
                peonData.Damage = 2;
                peonData.AtkRange = 2;
                //peonData.PosicaoPeao[0, 0] = i;
                //peonData.PosicaoPeao[1, 0] = j;
                peonData.Player = 2;
                //cubo
                cubeTemp.GetComponent<CubeData>().updateFill();
                break;
            case 3:

                //criar peao
                posObjeto += cubeTemp.transform.position;
                peaoCriado = Instantiate(peon[0], posObjeto, Quaternion.identity, cubeTemp.transform);
                peaoCriado.GetComponent<MeshRenderer>().material = materiais[1];
                peonData = peaoCriado.GetComponent<PeonData>();
                //dar valor ao peao
                peonData.Health = 3;
                peonData.Move = 3;
                peonData.Damage = 2;
                peonData.AtkRange = 2;
                //peonData.PosicaoPeao[0, 0] = i;
                //peonData.PosicaoPeao[1, 0] = j;
                peonData.Player = 2;
                //cubo
                cubeTemp.GetComponent<CubeData>().updateFill();
                break;
           default:
                break;
        }
    }

    public void SpawnBoardObjects(int numCasas)
    {
        GameObject pickupCriado;

        //teste manual
        virtualBoard[0, numCasas / 2] = 1;
        virtualBoard[numCasas - 1, numCasas / 2] = 2;

        for (int i = 0; i <= numCasas - 1; ++i)
        {
            for (int j = 0; j <= numCasas - 1; ++j)
            {
                if (virtualBoard[i, j] != 1 && virtualBoard[i, j] != 2)
                    if (Random.Range(1, 101) <= 50)
                        virtualBoard[i, j] = Random.Range(3, 7);
            }
        }

        for (int i = 0; i <= numCasas - 1; ++i)
        {
            for (int j = 0; j <= numCasas - 1; ++j)
            {
                //peaoCriado = null;
                pickupCriado = null;
                Vector3 posObjeto = new Vector3(0, 0.90f, 0);
                switch (virtualBoard[i, j])
                {/*
                    case 1:
                    
                        //criar peao
                        posObjeto += cube2dVectorStorage[i, j].transform.position;
                        peaoCriado = Instantiate(peon[0], posObjeto, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        peaoCriado.GetComponent<MeshRenderer>().material = materiais[0];
                        peonData = peaoCriado.GetComponent<PeonData>();
                        //dar valor ao peao
                        peonData.Health = 3;
                        peonData.Move = 3;
                        peonData.Damage = 2;
                        peonData.AtkRange = 2;
                        peonData.PosicaoPeao[0, 0] = i;
                        peonData.PosicaoPeao[1, 0] = j;
                        peonData.Player = 1;
                        //cubo
                        cube2dVectorStorage[i, j].GetComponent<CubeData>().updateFill();

                        break;
                    case 2:

                        //criar peao
                        posObjeto += cube2dVectorStorage[i, j].transform.position;
                        peaoCriado = Instantiate(peon[0], posObjeto, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        peaoCriado.GetComponent<MeshRenderer>().material = materiais[1];
                        peonData = peaoCriado.GetComponent<PeonData>();
                        //dar valor ao peao
                        peonData.Health = 3;
                        peonData.Move = 3;
                        peonData.Damage = 2;
                        peonData.AtkRange = 2;
                        peonData.PosicaoPeao[0, 0] = i;
                        peonData.PosicaoPeao[1, 0] = j;
                        peonData.Player = 2;
                        //cubo
                        cube2dVectorStorage[i, j].GetComponent<CubeData>().updateFill();

                        break;
                        */
                    case 3:

                        //criar objeto
                        posObjeto += cube2dVectorStorage[i, j].transform.position;
                        pickupCriado = Instantiate(objetos[1], posObjeto, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        pickupData = pickupCriado.GetComponent<PickupData>();
                        //dar valor
                        //pickupData.BonusCura = 0;
                        //pickupData.BonusMove = 0;
                        //pickupData.BonusDice = 0;
                        if (Random.Range(1, 101) < 90)
                            pickupData.BonusBattle = 1;
                        else
                        {
                            pickupData.BonusBattle = 2;
                            pickupCriado.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        //cubo
                        cube2dVectorStorage[i, j].GetComponent<CubeData>().updateFill();

                        break;
                    case 4:

                        //criar objeto
                        posObjeto += cube2dVectorStorage[i, j].transform.position;
                        pickupCriado = Instantiate(objetos[2], posObjeto, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        pickupData = pickupCriado.GetComponent<PickupData>();
                        //dar valor
                        //pickupData.BonusBattle = 0;
                        //pickupData.BonusCura = 0;
                        //pickupData.BonusMove = 0;
                        if (Random.Range(1, 101) < 90)
                            pickupData.BonusDice = 1;
                        else
                        {
                            pickupData.BonusDice = 2;
                            pickupCriado.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        //cubo
                        cube2dVectorStorage[i, j].GetComponent<CubeData>().updateFill();

                        break;
                    case 5:

                        //criar objeto
                        posObjeto += cube2dVectorStorage[i, j].transform.position;
                        pickupCriado = Instantiate(objetos[3], posObjeto, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        pickupData = pickupCriado.GetComponent<PickupData>();
                        //dar valor
                        //pickupData.BonusBattle = 0;
                        //pickupData.BonusCura = 0;
                        //pickupData.BonusDice = 0;
                        if (Random.Range(1, 101) < 90)
                            pickupData.BonusMove = 1;
                        else
                        {
                            pickupData.BonusMove = 2;
                            pickupCriado.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        //cubo
                        cube2dVectorStorage[i, j].GetComponent<CubeData>().updateFill();

                        break;
                    case 6:

                        //criar objeto
                        posObjeto += cube2dVectorStorage[i, j].transform.position;
                        pickupCriado = Instantiate(objetos[4], posObjeto, Quaternion.identity, cube2dVectorStorage[i, j].transform);
                        pickupData = pickupCriado.GetComponent<PickupData>();
                        //dar valor
                        //pickupData.BonusBattle = 0;
                        //pickupData.BonusMove = 0;
                        //pickupData.BonusDice = 0;
                        if (Random.Range(1, 101) < 90)
                            pickupData.BonusCura = 1;
                        else
                        {
                            pickupData.BonusCura = 2;
                            pickupCriado.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        //cubo
                        cube2dVectorStorage[i, j].GetComponent<CubeData>().updateFill();

                        break;
                    default:

                        break;
                }
            }
        }
    }

    public void PrepareMov(GameObject peonTemp)
    {
        //get data
        peonData = peonTemp.GetComponent<PeonData>();
        int movimento = peonData.Move;
        int posPeaoCol = peonData.PosicaoPeao[0, 0];
        int posPeaoLin = peonData.PosicaoPeao[1, 0];
        //deixar cor normal / desabilitar todos
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = false;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        //trocar cor / habilitar movimentos possiveis
        for (int i = -movimento; i <= movimento; ++i)
        {
            for (int j = -movimento; j <= movimento; ++j)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= movimento && (posPeaoCol + i >= 0 && posPeaoLin + j >= 0 && posPeaoCol + i <= numeroCasas - 1 && posPeaoLin + j <= numeroCasas - 1))
                {
                    cube2dVectorStorage[posPeaoCol + i, posPeaoLin + j].GetComponent<BoxCollider>().enabled = true;
                    cube2dVectorStorage[posPeaoCol + i, posPeaoLin + j].GetComponent<Renderer>().material.color = new Color(0, 0, 0.50f, 1);
                }
            }
        }
    }

    public void PrepareAtk(GameObject peonTemp)
    {
        //get data
        peonData = peonTemp.GetComponent<PeonData>();
        int atkRange = peonData.AtkRange;
        int posPeaoCol = peonData.PosicaoPeao[0, 0];
        int posPeaoLin = peonData.PosicaoPeao[1, 0];
        //deixar cor normal / desabilitar todos
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = false;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        //trocar cor / habilitar batalhas possiveis
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

    public void LimparTabuleiro()
    {
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = true;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
    }

    public void pickUpPickup(GameObject peaoTemp, GameObject pickUpTemp)
    {
        //realisar ajuste valores
        var pickupData = pickUpTemp.GetComponent<PickupData>();
        var peaoData = peaoTemp.GetComponent<PeonData>();

        if (pickupData.BonusDice > 0)
        {
            gameManager.NumberDice += pickupData.BonusDice;
            Debug.Log("Mais " + pickupData.BonusDice + " Dado(s) de Acerto!");
        }
        if (pickupData.BonusBattle > 0)
        {
            gameManager.BattleAmount += pickupData.BonusBattle;
            Debug.Log("Mais " + pickupData.BonusBattle + " Fase(s) de Batalha!");
        }
        if (pickupData.BonusMove > 0)
        {
            gameManager.MoveAmount += pickupData.BonusMove;
            Debug.Log("Mais " + pickupData.BonusMove + " Fase(s) de Movimento!");
        }
        if (pickupData.BonusCura > 0)
        {
            peaoData.Health += pickupData.BonusCura;
            Debug.Log("O peao foi curado em: " + pickupData.BonusCura);
        }
    }

    public void Movimento(GameObject peaoTemp, GameObject cuboTemp, GameObject cuboPeao)
    {
        StartCoroutine(PeonMoveAnim(peaoTemp, cuboTemp, cuboPeao));
    }

    public void Ataque(GameObject peaoTemp, GameObject alvoTemp, GameObject cuboTemp)
    {
        //tirar batalha
        gameManager.BattleAmount--;

        //pegar data
        int[] player1Atk = new int[gameManager.NumberDice];
        int[] player2Atk = new int[3];
        int vida = alvoTemp.GetComponent<PeonData>().Health;
        int dano = peaoTemp.GetComponent<PeonData>().Damage;
        int numDice = gameManager.NumberDice;
        int atkOk = 0;
        Debug.Log(numDice);
        //rolar dados
        for (int i = 0; i < numDice; i++)
        {
            player1Atk[i] = Random.Range(1, 7);

            if (i < 3)
            {
                player2Atk[i] = Random.Range(1, 7);
            }
        }
        System.Array.Sort(player1Atk);
        System.Array.Sort(player2Atk);
        //realisar batalha
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log(numDice + (-i - 1));
            Debug.Log("|P1: " + player1Atk[numDice + (-i - 1)] + "|  vs  |" + "P2: " + player2Atk[2 - i] + "|");
            if (player1Atk[numDice + (-i - 1)] >= player2Atk[2 - i])
            {
                atkOk++;
            }
        }
        //resutado batalha
        if (atkOk >= 2)
        {
            vida = vida - dano;
            alvoTemp.GetComponent<PeonData>().Health = vida;
            alvoTemp.GetComponent<PeonData>().Checkhealth();
            if (vida < 0) vida = 0;
            Debug.Log("ATAQUE CONECTOU!!!!");
            Debug.Log("Oponente leva " + dano + " de dano!");
            if (vida > 0) Debug.Log("Oponente ainda tem mais " + vida + " de vida");
        }
        else
        {
            Debug.Log("ataque desviado!");
        }



        //cubos
        cuboTemp.GetComponent<CubeData>().updateFill();
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = true;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

        //gamemanager
        if (gameManager.BattleAmount > 0) Debug.Log(gameManager.BattleAmount + " Batalha(s) Sobrando!");
        else Debug.Log("Batalha acabada");
        gameManager.BattlePhase = false;
        gameManager.endTurnCheck();

    }

    IEnumerator PeonMoveAnim(GameObject peaoTemp, GameObject cuboTemp, GameObject cuboPeao)
    {
        //tirar acao
        gameManager.MoveAmount--;

        //pegar data
        peonData = peaoTemp.GetComponent<PeonData>();
        cubeData = cuboTemp.GetComponent<CubeData>();
        float elapsedTime = 0;
        float waitTime = 0.5f;
        Vector3 peaoPos = peaoTemp.transform.position;
        Vector3 cuboPos = cuboTemp.transform.position;
        Vector3 up = new Vector3(0, 0.90f, 0);

        //movimento
        while (elapsedTime < waitTime)
        {
            peaoTemp.transform.position = Vector3.Lerp(peaoPos, cuboPos + up, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //destruir pickup caso tenha
        foreach (Transform child in cuboTemp.transform)
        {
            if (child.tag == "Pickup")
                Destroy(child.gameObject);
        }
        //cubo
        peaoTemp.transform.parent = cuboTemp.transform;
        cuboPeao.GetComponent<CubeData>().updateFill();
        cuboTemp.GetComponent<CubeData>().updateFill();
        foreach (GameObject a in cubeVectorStorage)
        {
            a.GetComponent<BoxCollider>().enabled = true;
            a.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        //peao

        peonData.PosicaoPeao[0, 0] = cubeData.CuboCordenada[0, 0];
        peonData.PosicaoPeao[1, 0] = cubeData.CuboCordenada[1, 0];

        //gameManager
        if (gameManager.MoveAmount > 0) Debug.Log(gameManager.MoveAmount + " Movimento(s) Sobrando!");
        else Debug.Log("Movimento acabado");
        gameManager.MovePhase = false;
        gameManager.endTurnCheck();

    }
}
