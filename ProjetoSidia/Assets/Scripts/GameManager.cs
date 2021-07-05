using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int numeroCasas = 16;
    private int pecasJogador1 = 3;
    private int pecasJogador2 = 3;
    public static bool singlePlayer;
    public GridManager gridManager;
    bool deployPhase;

    private int moveAmount = 2;
    private int battleAmount = 1;
    private bool movePhase = false;
    private bool battlePhase = false;
    private int numberDice = 3;
    private int turnoJogador = 1;
    private bool spawnPhase = true;
    private int spawnAmount = 6;
    private bool mouseLock = false;

    int countClass = 0;
    int countClass2 = 1;

    GameObject cuboPeao;
    GameObject peao;
    GameObject cubo;    
    GameObject alvo = null;
    GameObject pickUp;

    public Text [] text;

    public GameObject image;

    public int MoveAmount { get => moveAmount; set => moveAmount = value; }
    public int BattleAmount { get => battleAmount; set => battleAmount = value; }
    public bool MovePhase { get => movePhase; set => movePhase = value; }
    public bool BattlePhase { get => battlePhase; set => battlePhase = value; }
    public int NumberDice { get => numberDice; set => numberDice = value; }
    public int TurnoJogador { get => turnoJogador; set => turnoJogador = value; }
    public bool SpawnPhase { get => spawnPhase; set => spawnPhase = value; }
    public int SpawnAmount { get => spawnAmount; set => spawnAmount = value; }
    public bool MouseLock { get => mouseLock; set => mouseLock = value; }

    public int PecasJogador1 { get => pecasJogador1; set => pecasJogador1 = value; }
    public int PecasJogador2 { get => pecasJogador2; set => pecasJogador2 = value; }

    void awake()
    {
       
    }

    void Start()
    {
        TurnoJogador = Random.Range(1, 3);
        //criar tabuleiro
        gridManager.CreateGrid(numeroCasas, TurnoJogador);
    }

    void Update()   
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Selecionar peao para movimentar /usar para movimento /usar para batalha
        if (Input.GetMouseButtonDown(0))
        {
            if (SpawnPhase == false)
            {
                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().CuboFillId == 1 && MoveAmount > 0 && MovePhase == false)
                {                   
                    cuboPeao = hit.transform.gameObject;
                    cuboPeao.GetComponent<CubeData>().UpdateFill();
                    peao = cuboPeao.transform.GetChild(0).gameObject;
                    if (peao.GetComponent<PeonData>().Player == TurnoJogador)
                    {
                        gridManager.PrepareMov(peao);
                        MovePhase = true;
                        BattlePhase = false;
                    }
                }
                else if(Physics.Raycast(ray, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().CuboFillId != 1 && MovePhase == true)
                {

                    cubo = hit.transform.gameObject;
                    cubo.GetComponent<CubeData>().UpdateFill();
                    if (cubo.transform.childCount != 0)
                    {
                        pickUp = cubo.transform.GetChild(0).gameObject;
                        gridManager.PickUpPickup(peao, pickUp);
                    }
                    gridManager.Movimento(peao, cubo, cuboPeao);

                }
                else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().CuboFillId == 1 && BattleAmount > 0 && BattlePhase == true)
                {                  
                    cubo = hit.transform.gameObject;
                    alvo = cubo.transform.GetChild(0).gameObject;
                    gridManager.Ataque(peao, alvo, cubo);
                }
            }
            else
            {

                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().CuboFillId == 0)
                {
                    SpawnAmount--;
                    countClass++;
                    cubo = hit.transform.gameObject;
                    gridManager.SpawnPlayerPeons(cubo, turnoJogador, countClass2);
                    if (countClass == 2)
                    {
                        countClass = 0;
                        countClass2++;
                    }

                }
            }
        }
        //Selecionar peao para batalha
        if (Input.GetMouseButtonDown(1) && SpawnPhase == false)
        {
            if (SpawnPhase == false)
            {
                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Cubo" && hit.transform.gameObject.GetComponent<CubeData>().CuboFillId == 1 && BattleAmount > 0 && BattlePhase == false)
                {
                    cuboPeao = hit.transform.gameObject;
                    peao = cuboPeao.transform.GetChild(0).gameObject;
                    Debug.Log(TurnoJogador);
                    if (peao.GetComponent<PeonData>().Player == TurnoJogador)
                    {
                        gridManager.PrepareAtk(peao);
                        BattlePhase = true;
                        MovePhase = false;
                    }
                }
            }
        }
        //passar de turno
        if (Input.GetKeyDown(KeyCode.Return) && SpawnPhase == false)
        {
            moveAmount = 0;
            battleAmount = 0;
            EndTurnCheck();
        }

        
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Cubo" && hit.transform.gameObject.transform.childCount == 1)
        {
            if (hit.transform.gameObject.transform.GetChild(0).tag == "Peon") {
                var tempCubo = hit.transform.gameObject.transform.GetChild(0);
                text[0].text = "Velocidade: " + "\n" + tempCubo.GetComponent<PeonData>().Move + "\n" +
                        "alcance: " + "\n" + tempCubo.GetComponent<PeonData>().AtkRange + "\n" +
                        "dano: " + "\n" + tempCubo.GetComponent<PeonData>().Damage + "\n" +
                        "vida: " + "\n" + tempCubo.GetComponent<PeonData>().Health + "\n";
            }
            else
            {
                text[0].text = "Velocidade: " + "\n" + "\n" +
                        "alcance: " + "\n" + "\n" +
                        "dano: " + "\n" +  "\n" +
                        "vida: " + "\n" +  "\n";
            }
        }
    }

    
    //termino de turno altomatico
    public void EndTurnCheck()
    {
        if(BattleAmount <= 0 && MoveAmount <=0)
        {
            StartCoroutine(FinalizarTurno());
        }
    }

    public void TextoTurno()
    {
        text[1].text = "JOGADOR: " + "\n" +TurnoJogador+ "\n" +
                        "Movimento: " + "\n" + MoveAmount + "\n" +
                        "Batalha: " + "\n" + BattleAmount + "\n";
    }

    public void EndGameCheck ()
    {
        if (PecasJogador1 == 0)
        {
            image.SetActive(true);
            text[2].text = "JOGADOR 1 VENCEU";
        }
        else if (PecasJogador2 == 0)
        {
            image.SetActive(true);
            text[2].text = "JOGADOR 2 VENCEU";
        }
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator FinalizarTurno()
    {
        yield return new WaitForSeconds(0.5f);
        gridManager.LimparTabuleiro();
        MoveAmount = 2;
        BattleAmount = 1;
        NumberDice = 3;
        BattlePhase = false;
        MovePhase = false;
        if (TurnoJogador == 1) TurnoJogador = 2;
        else TurnoJogador = 1;
        gridManager.ArrumarCamera(TurnoJogador);
        TextoTurno();

    }
}
