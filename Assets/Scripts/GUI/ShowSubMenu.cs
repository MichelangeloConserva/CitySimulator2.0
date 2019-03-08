using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSubMenu : MonoBehaviour
{
    public GameObject buildSubMenu;
    public GameObject roadsSubMenu;
    public GameObject businessSubMenu;
    public GameObject deletingSubMenu;
    public GameObject garbageSubMenu;
    public GameObject housesSubMenu;

    Vector3 hidden = new Vector3(-1000, -1000, 0);
    Vector3 show = new Vector3(-375, -155, 0);

    private void Awake()
    {
        buildSubMenu.transform.localPosition = show;
        roadsSubMenu.transform.localPosition = hidden;
        deletingSubMenu.transform.localPosition = hidden;
        garbageSubMenu.transform.localPosition = hidden;
        housesSubMenu.transform.localPosition = hidden;
    }

    public void ParseRequest(string subMenu)
    {
        if (subMenu == "New Road")
        {
            buildSubMenu.transform.localPosition = hidden;
            roadsSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Deleting")
        {
            buildSubMenu.transform.localPosition = hidden;
            deletingSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Back Deleting")
        {
            deletingSubMenu.transform.localPosition = hidden;
            buildSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Back Roads")
        {
            roadsSubMenu.transform.localPosition = hidden;
            buildSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Garbage")
        {
            buildSubMenu.transform.localPosition = hidden;
            garbageSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Houses")
        {
            buildSubMenu.transform.localPosition = hidden;
            housesSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Back Garbage")
        {
            garbageSubMenu.transform.localPosition = hidden;
            buildSubMenu.transform.localPosition = show;
        }

        if (subMenu == "Back Houses")
        {
            housesSubMenu.transform.localPosition = hidden;
            buildSubMenu.transform.localPosition = show;
        }
    }
}
