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
    public AudioSource click;   

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
        click.Play();
        Debug.Log("multiplayer");
        StartCoroutine(ImageChange(0.5f));
        GameManager.singlePlayer = true;
    }

    public void vsAi()
    {
        click.Play();
        Debug.Log("2");
        //botoesPrinc.SetActive(false);
        //botoesTam.SetActive(false);

    }

    public void menuOpcoes()
    {
        click.Play();
        Debug.Log("3");
        StartCoroutine(MenuChange(0.5f));

    }

    public void menuAjuda()
    {
        click.Play();
        Debug.Log("4");
        StartCoroutine(MenuChange(0.5f));
    }

    public void sairJogo()
    {
        click.Play();
        Debug.Log("exitgame");
        Application.Quit();
    }

    public void Tam6x6()
    {
        Debug.Log("66");
        click.Play();
        SceneManager.LoadScene(1);
        GameManager.numeroCasas = 6;
    }

    public void Tam10x10()
    {
        Debug.Log("1010");
        click.Play();
        SceneManager.LoadScene(1);
        GameManager.numeroCasas = 10;
    }

    public void Tam16x16()
    {
        Debug.Log("1616");
        click.Play();
        SceneManager.LoadScene(1);
        GameManager.numeroCasas = 16;
    }

    public void VoltarBotMen()
    {
        click.Play();
        StartCoroutine(ImageChange(0.5f));
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
    IEnumerator ImageChange(float delayTime)
    {
        if (botoesTam.activeSelf == true)
        {
            botoesPrinc.SetActive(true);
            botoesTam.SetActive(false);
            yield return new WaitForSeconds(delayTime);
        }
        else
        {
            botoesPrinc.SetActive(false);
            botoesTam.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
    }
}