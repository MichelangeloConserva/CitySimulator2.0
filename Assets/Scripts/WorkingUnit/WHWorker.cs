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


    public WHWorker(int adultIndex, HUEconomy myHUEconomy, HUCarsHandler myHUCarsHandler, System.DateTime workingStart, int workingHours, NodeStreet workSpawn)
    {
        this.myHUEconomy = myHUEconomy;
        this.myHUCarsHandler = myHUCarsHandler;
        this.workingStart = workingStart;
        this.workingHours = workingHours;
        this.adultIndex = adultIndex;
        this.workSpawn = workSpawn;
    }

    public void EarnMoney(float retribution)
    {
        myHUEconomy.savings += retribution;
    }

    public void GoHome()
    {
        myHUCarsHandler.adultsAtWork[adultIndex] = false;

        myHUCarsHandler.WorkerMoving(workSpawn, myHUCarsHandler.spawnPoint);
    }



}
