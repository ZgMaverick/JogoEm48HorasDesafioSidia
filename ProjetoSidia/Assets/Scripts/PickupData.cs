using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupData : MonoBehaviour
{
    private int bonusCura = 0;
    private int bonusDice = 0;
    private int bonusBattle = 0;
    private int bonusMove = 0;

    public int BonusCura { get => bonusCura; set => bonusCura = value; }
    public int BonusDice { get => bonusDice; set => bonusDice = value; }
    public int BonusBattle { get => bonusBattle; set => bonusBattle = value; }
    public int BonusMove { get => bonusMove; set => bonusMove = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
