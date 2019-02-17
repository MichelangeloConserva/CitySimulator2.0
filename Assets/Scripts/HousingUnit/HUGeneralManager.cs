using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class HUGeneralManager : MonoBehaviour
{
    public int numberOfHU = 0;

    public List<HUInitFamily> HUs = new List<HUInitFamily>();
    public int numAdults;

    public void DayTimeChanged(DayTime dayTime)
    {
        foreach(HUInitFamily hu in HUs)
            hu.huEconomy.DayTimeCostsUpdate(dayTime);

    }


    void OnGUI()
    {
        if (numAdults!=0)
            GUI.Label(new Rect(10, 45, 400, 20), "Adults with car in the city: "+ numAdults);
    }

}
