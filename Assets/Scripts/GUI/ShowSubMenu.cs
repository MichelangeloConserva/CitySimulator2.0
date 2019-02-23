using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSubMenu : MonoBehaviour
{
    public GameObject buildSubMenu;
    public GameObject roadsSubMenu;
    public GameObject businessSubMenu;
    public GameObject deletingSubMenu;

    Vector3 hidden = new Vector3(-1000, -1000, 0);
    Vector3 show = new Vector3(-540, -215, 0);

    private void Awake()
    {
        buildSubMenu.transform.localPosition = show;
        roadsSubMenu.transform.localPosition = hidden;
        deletingSubMenu.transform.localPosition = hidden;
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
    }
}
