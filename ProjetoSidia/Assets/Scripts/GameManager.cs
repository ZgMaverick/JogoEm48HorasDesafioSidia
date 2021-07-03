using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numeroCasas = 6;
    public static bool singlePlayer;
    public GridManager gridManager;
    bool deployPhase;
    int movePhase;
    int battlePhase;
    int numberDice;
    bool turnOne;

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
            if (Physics.Raycast(mouse, out hit))
            {
                Debug.Log(hit.transform.name);
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
