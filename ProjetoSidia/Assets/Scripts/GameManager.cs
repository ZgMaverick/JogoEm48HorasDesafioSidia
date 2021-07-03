using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numeroCasas = 6;
    public static bool singlePlayer;
    public GridManager gridManager;
    bool deployPhase;
    int moveAmount = 1;
    int battleAmount = 1;
    bool movePhase = false;
    bool battlePhase = false;
    int numberDice = 3;
    bool turnEven = true;
    GameObject peao;
    GameObject cubo;

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
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Peon" && moveAmount>0 && movePhase == false)
            {
                peao = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                //Debug.Log(hit.transform.name);
                gridManager.PrepareMov(peao);
                movePhase = true;
                battlePhase = false;
            }
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && movePhase == true)    
            {
                cubo = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                Debug.Log(hit.transform.name);
                //gridManager.Movimento(cubo);

            }
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Cubo" && battlePhase == true)
            {
                cubo = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                Debug.Log(hit.transform.name);
                //gridManager.Movimento(cubo);

            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Peon" && battleAmount>0 && battlePhase == false)
            {
                peao = hit.transform.gameObject;
                //Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                //Debug.Log(hit.transform.name);
                gridManager.PrepareAtk(peao);
                battlePhase = true;
                movePhase = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(numeroCasas);
            Debug.Log(singlePlayer);
            //Debug.Log(posInc);
            //Debug.Log(posFnl);

        }
    }
}
