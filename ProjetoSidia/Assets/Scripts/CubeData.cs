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
        foreach (Transform child in transform)
        {
            if (child.tag == "Pickup")
            {
                cuboFillId = 2;
            }
            else if(child.tag == "Peon")
            {
                cuboFillId = 1;
            }
            else
            {
                cuboFillId = 0;
            }
        }
    }
}
