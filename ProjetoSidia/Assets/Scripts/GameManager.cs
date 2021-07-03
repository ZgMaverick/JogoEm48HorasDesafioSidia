using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numeroCasas = 6  ;
    public static bool singlePlayer;
    public GridManager gridManager;
    bool deployPhase;
    int movePhase;
    int battlePhase;
    int numberDice;
    bool turnOne;
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
            if (Physics.Raycast(mouse, out hit) && hit.transform.tag == "Peon")
            {
                peao = hit.transform.gameObject;
                Debug.Log(peao.GetComponent<PeonData>().PosicaoPeao[0, 0] + peao.GetComponent<PeonData>().PosicaoPeao[1, 0]);
                Debug.Log(hit.transform.name);
                gridManager.PrepareMov(peao);
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
