using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeData : MonoBehaviour
{
    private int[,] cuboCordenada = new int[2, 1];
    public int cuboFillId = 0;

    public int[,] CuboCordenada { get => cuboCordenada; set => cuboCordenada = value; }
    public int CuboFillId { get => cuboFillId; set => cuboFillId = value; }

  
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateFill()
    {
        if (this.transform.childCount == 0)
        {
            CuboFillId = 0;
        }
        else if (this.transform.GetChild(0).tag == "Pickup")
        {
            CuboFillId = 2;
        }
        else if (this.transform.GetChild(0).tag == "Peon")
        {
            CuboFillId = 1;
        }

    }
}
