using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Menu menuAtual;
    public Menu menuPrincipal;
    public Menu menuConfig;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu(menuAtual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu(Menu menu)
    {
        if (menuAtual != null)
            menuAtual.IsOpen = false;
        menuAtual = menu;
        menuAtual.IsOpen = true;
    }
    public void HideMenu(Menu menu)
    {
        if (menuAtual != null)
            menuAtual.IsOpen = false;
        menuAtual = menu;
        menuAtual.IsOpen = false;
    }
}
