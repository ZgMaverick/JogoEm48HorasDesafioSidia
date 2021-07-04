using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numeroCasas = 16;
    public static bool singlePlayer;
    public GridManager gridManager;
    bool deployPhase;
    private int moveAmount = 2;
    private int battleAmount = 1;
    private bool movePhase = false;
    private bool battlePhase = false;
    private int numberDice = 3;
    private int turnoJogador = 1;
    private bool spawnPhase = true;
    private int spawnAmount = 6;
    private bool mouseLock = false;

    GameObject cuboPeao;
    GameObject peao;
    GameObject cubo;
    GameObject alvo;
    GameObject pickUp;

    public int MoveAmount { get => moveAmount; set => moveAmount = value; }
    public int BattleAmount { get => battleAmount; set => battleAmount = value; }
    public bool MovePhase { get => movePhase; set => movePhase = value; }
    public bool BattlePhase { get => battlePhase; set => battlePhase = value; }
    public int NumberDice { get => numberDice; set => numberDice = value; }
    public int TurnoJogador { get => turnoJogador; set => turnoJogador = value; }
    public bool SpawnPhase { get => spawnPhase; set => spawnPhase = value; }
    public int SpawnAmount { get => spawnAmount; set => spawnAmount = value; }
    public bool MouseLock { get => mouseLock; set => mouseLock = value; }

    void awake()
    {
       
    }

    void Start()
    {
        TurnoJogador = Random.Range(1, 3);
        //criar tabuleiro
        gridManager.CreateGrid(numeroCasas, TurnoJogador);
    }

    void Update()   
    {
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Selecionar peao para movimentar /usar para movimento /usar para batalha
        if (Input.GetMouseButtonDown(0))
        {
            if (SpawnPhase == false)
            {
                if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().cuboFillId == 1 && MoveAmount > 0 && MovePhase == false)
                {
                    cuboPeao = hit.transform.gameObject;
                    peao = cuboPeao.transform.GetChild(0).gameObject;
                    if (peao.GetComponent<PeonData>().Player == TurnoJogador)
                    {
                        gridManager.PrepareMov(peao);
                        MovePhase = true;
                    }
                }
                if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().cuboFillId != 1 && MovePhase == true)
                {

                    cubo = hit.transform.gameObject;
                    if (cubo.transform.childCount != 0)
                    {
                        pickUp = cubo.transform.GetChild(0).gameObject;
                        gridManager.pickUpPickup(peao, pickUp);
                    }
                    gridManager.Movimento(peao, cubo, cuboPeao);

                }
                if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().cuboFillId == 1 && BattleAmount > 0 && BattlePhase == true)
                {

                    cubo = hit.transform.gameObject;
                    alvo = cuboPeao.transform.GetChild(0).gameObject;
                    gridManager.Ataque(peao, alvo, cubo);

                }
            }
            else
            {
                if (0 >= SpawnAmount)
                {
                    SpawnPhase = false;
                    gridManager.LimparTabuleiro();
                }
                if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().cuboFillId == 0)
                {
                    SpawnAmount--;
                    cubo = hit.transform.gameObject;
                    gridManager.SpawnPlayerPeons(cubo, turnoJogador, 1);    
                    
                }
            }
        }
        //Selecionar peao para batalha
        if (Input.GetMouseButtonDown(1))
        {
            if (SpawnPhase == false)
            {
                if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().cuboFillId == 1 && BattleAmount > 0 && BattlePhase == false)
                {
                    cuboPeao = hit.transform.gameObject;
                    peao = cuboPeao.transform.GetChild(0).gameObject;
                    Debug.Log(TurnoJogador);
                    if (peao.GetComponent<PeonData>().Player == TurnoJogador)
                    {
                        gridManager.PrepareAtk(peao);
                        BattlePhase = true;
                        MovePhase = false;
                    }
                }
            }
        }
        //passar de turno
        if (Input.GetKeyDown(KeyCode.Return))
        {
            moveAmount = 0;
            battleAmount = 0;
            endTurnCheck();
        }
    }
    //termino de turno altomatico
    public void endTurnCheck()
    {
        if(BattleAmount <= 0 && MoveAmount <=0)
        {
            gridManager.LimparTabuleiro();
            MoveAmount = 2;
            BattleAmount = 1;
            NumberDice = 3;
            BattlePhase = false;
            MovePhase = false;
            if (TurnoJogador == 1)TurnoJogador = 2;
            else TurnoJogador = 1;

        }
    }
}
