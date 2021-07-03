using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    private int[,] cuboCordenada = new int[1, 2];
    private int cuboFillId = 0;

    public int[,] CuboCordenada { get => cuboCordenada; set => cuboCordenada = value; }
    public int CuboFillId { get => cuboFillId; set => cuboFillId = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
