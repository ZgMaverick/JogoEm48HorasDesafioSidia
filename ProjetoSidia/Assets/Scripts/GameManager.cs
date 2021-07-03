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
    int numberDice = 3;
    bool turnEven = true;
    GameObject peao;
    GameObject cubo;

    public int MoveAmount { get => moveAmount; set => moveAmount = value; }
    public int BattleAmount { get => battleAmount; set => battleAmount = value; }
    public bool MovePhase { get => movePhase; set => movePhase = value; }
    public bool BattlePhase { get => battlePhase; set => battlePhase = value; }

    // Start is called before the first frame update\

    void awake()
    {
       
    }

    private void Start()
    {
        gridManager.CreateGrid(numeroCasas);
        //gridManager.SpawnBoardObjects(numeroCasas);
    }
    // Update is called once per frame
    void Update()   
    {
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Peon" && MoveAmount>0 && MovePhase == false)
            {
                peao = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                //Debug.Log(hit.transform.name);
                gridManager.PrepareMov(peao);
                MovePhase = true;
                BattlePhase = false;
            }
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && MovePhase == true)    
            {
                cubo = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                Debug.Log(hit.transform.name);
                gridManager.Movimento(peao,cubo);

            }
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && BattlePhase == true)
            {
                cubo = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                Debug.Log(hit.transform.name);
                //gridManager.Movimento(cubo);

            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Peon" && BattleAmount>0 && BattlePhase == false)
            {
                peao = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                //Debug.Log(hit.transform.name);
                gridManager.PrepareAtk(peao);
                BattlePhase = true;
                MovePhase = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log(numeroCasas);
            //Debug.Log(singlePlayer);
            Debug.Log(peao.transform.position);
            //Debug.Log(posFnl);

        }
    }
}
