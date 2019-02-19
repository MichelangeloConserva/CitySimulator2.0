using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSubMenu : MonoBehaviour
{
    public GameObject buildingSubMenu;
    public GameObject roadsSubMenu;
    public GameObject businessSubMenu;


    public void ParseRequest(string subMenu)
    {
        if (subMenu == "New Road")
        {
            Destroy(gameObject.transform.Find("BuildingGUI").gameObject);
            Instantiate(roadsSubMenu,this.gameObject.transform);
        }
    }
}
