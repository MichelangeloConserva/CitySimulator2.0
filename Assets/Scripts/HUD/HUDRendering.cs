using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDRendering : MonoBehaviour
{

    public Player p;




    void OnGUI()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(Screen.width - 100, 10, 400, 100), "Money: " + p.money, style);
    }

}
