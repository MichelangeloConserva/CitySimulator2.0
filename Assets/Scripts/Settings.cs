using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{

    public static readonly bool visualizeRoadNetwork = false;

    public static float timeForLightChange = 7;
    public static int timeMultiplyer = 60;


    public static float speedLimit = 55;


    public enum TrafficLightLights
    {
        red,
        yellow,
        green
    }


}
