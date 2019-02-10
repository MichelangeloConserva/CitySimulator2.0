using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManagementTime : MonoBehaviour
{
    public static System.DateTime realTime;

    void Start()
    {
        realTime = new System.DateTime(System.DateTime.Now.Year,
                                       System.DateTime.Now.Month,
                                       System.DateTime.Now.Day,
                                       6, // hours
                                       0, // minutes
                                       0);// seconds
    }

    void Update()
    {
        realTime = realTime.AddSeconds(Time.deltaTime * 60 * 5); 
    }

    void OnGUI()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(10, 30, 400, 100), realTime.ToString("HH:mm:ss"), style);
    }
}
