using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public MenuManager menuManager;
    private Animator menuAnimator;
    private bool isOpen;
    public GameObject botoesPrinc;
    public GameObject botoesTam;

    public bool IsOpen
    {

        get { return menuAnimator.GetBool("IsOpen"); }
        set { menuAnimator.SetBool("IsOpen", value); }

    }

    void Awake()
    {
        menuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void multiPlayer()
    {

        Debug.Log("multiplayer");
        botoesPrinc.SetActive(false);
        botoesTam.SetActive(true);
        GameManager.singlePlayer = true;
    }

    public void vsAi()
    {

        Debug.Log("2");
        //botoesPrinc.SetActive(false);
        //botoesTam.SetActive(false);

    }

    public void menuOpcoes()
    {

        Debug.Log("3");
        StartCoroutine(MenuChange(0.5f));

    }

    public void menuAjuda()
    {

        Debug.Log("4");
        StartCoroutine(MenuChange(0.5f));
    }

    public void sairJogo()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }

    public void Tam6x6()
    {
        Debug.Log("66");

        SceneManager.LoadScene(1);
        GameManager.numeroCasas = 6;
    }

    public void Tam10x10()
    {
        Debug.Log("1010");

        SceneManager.LoadScene(1);
        GameManager.numeroCasas = 10;
    }

    public void Tam16x16()
    {
        Debug.Log("1616");

        SceneManager.LoadScene(1);
        GameManager.numeroCasas = 16;
    }

    public void VoltarBotMen()
    {
        botoesPrinc.SetActive(true);
        botoesTam.SetActive(false);

    }

    IEnumerator MenuChange(float delayTime)
    {
        if (menuManager.menuAtual.name == menuManager.menuConfig.name)
        {
            Debug.Log("teste 1");
            menuManager.HideMenu(menuManager.menuConfig);
            yield return new WaitForSeconds(delayTime);
            menuManager.ShowMenu(menuManager.menuPrincipal);
        }
        else
        {
            Debug.Log("teste 2");
            menuManager.HideMenu(menuManager.menuPrincipal);
            yield return new WaitForSeconds(delayTime);
            menuManager.ShowMenu(menuManager.menuConfig);

        }
    }
}
