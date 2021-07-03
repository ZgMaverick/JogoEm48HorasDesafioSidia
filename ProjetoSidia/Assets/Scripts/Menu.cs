using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{   
    public MenuManager menuManager;
    private Animator menuAnimator;
    private bool isOpen;

    public bool IsOpen {  

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

    void multiPlayer(){
        Debug.Log("1");
    }
    void vsAi()
    {
        Debug.Log("2");
    }
    void menuOpcoes()
    {
        Debug.Log("3");
    }
    void menuAjuda()
    {
        Debug.Log("4");
    }
    void sairJogo()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }
}
