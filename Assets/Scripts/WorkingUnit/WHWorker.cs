using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHWorker
{
    public int workingHours;

    private HUEconomy myHUEconomy;
    private HUCarsHandler myHUCarsHandler;
    private System.DateTime workingStart;
    private int adultIndex;
    private NodeStreet workSpawn;
    private Quaternion rot;


    public WHWorker(int adultIndex, HUEconomy myHUEconomy, HUCarsHandler myHUCarsHandler, System.DateTime workingStart, int workingHours, NodeStreet workSpawn, Quaternion rot)
    {
        this.myHUEconomy = myHUEconomy;
        this.myHUCarsHandler = myHUCarsHandler;
        this.workingStart = workingStart;
        this.workingHours = workingHours;
        this.adultIndex = adultIndex;
        this.workSpawn = workSpawn;
        this.rot = rot;
    }

    public void EarnMoney(float retribution)
    {
        myHUEconomy.savings += retribution;
    }

    public void GoHome()
    {
        myHUCarsHandler.adultsAtWork[adultIndex] = false;
                                               // TODO : refactor
        myHUCarsHandler.WorkerMoving(workSpawn, myHUCarsHandler.huInitFamily.GetspawnPoint(), rot);
    }



}
