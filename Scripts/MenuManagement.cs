using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour
{
    [SerializeField] Menu[] menus;
    public static MenuManagement Instance;

    public void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string MenuName)
    { //to open the preferred menu and if it is already open it will close it.
        for(int i=0; i<menus.Length; i++)
        {
            if(menus[i].MenuName == MenuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for(int i=0; i<menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
