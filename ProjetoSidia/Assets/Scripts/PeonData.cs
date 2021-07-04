using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonData : MonoBehaviour
{
    private int health;
    private int move;
    private int damage;
    private int atkRange;
    private int diceBonus;
    private int[,] posicaoPeao = new int[2, 1];
    private int player;

    public int Health { get => health; set => health = value; }
    public int Move { get => move; set => move = value; }
    public int Damage { get => damage; set => damage = value; }
    public int AtkRange { get => atkRange; set => atkRange = value; }
    public int Player { get => player; set => player = value; }
    public int[,] PosicaoPeao { get => posicaoPeao; set => posicaoPeao = value; }
    public int DiceBonus { get => diceBonus; set => diceBonus = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Checkhealth()
    {
        if (health <= 0)
        {
            Debug.Log("Peao do jogador " + player + " foi morto");
            Destroy(this.gameObject);
        }
    }

}
