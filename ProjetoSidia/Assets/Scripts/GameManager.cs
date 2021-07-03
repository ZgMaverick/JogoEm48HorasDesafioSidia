using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numeroCasas = 6;
    public static bool singlePlayer;
    public GridManager gridManager;
    // Start is called before the first frame update\
    
    void Start()
    {
        gridManager.CreateGrid(numeroCasas);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(numeroCasas);
            Debug.Log(singlePlayer);
            //Debug.Log(posInc);
            //Debug.Log(posFnl);

        }
    }
}
