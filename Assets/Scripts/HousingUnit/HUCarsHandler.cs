﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUCarsHandler : MonoBehaviour
{

    [HideInInspector]
    public HUInitFamily huInitFamily;

    [HideInInspector]
    public NodeStreet spawnPoint;

    public CarsManager carsManager;


    public System.DateTime[] dtWorkingLeaving;
    public bool[] adultsAtWork;
    public GameObject[] workingPlaces;

    void Update()
    {
        if (dtWorkingLeaving != null)
            for (int adult = 0; adult < huInitFamily.numberOfAdultsComponents; adult++)
                if (!adultsAtWork[adult])
                {
                    StartCoroutine(GoToWork(adult));
                }
    }

    private IEnumerator GoToWork(int adult)
    {

        if (dtWorkingLeaving[adult].Hour == CityManagementTime.realTime.Hour &
                       (dtWorkingLeaving[adult].Minute > CityManagementTime.realTime.Minute && dtWorkingLeaving[adult].Minute < CityManagementTime.realTime.Minute + 5))
        {
            adultsAtWork[adult] = true;

            var endNode = workingPlaces[adult].GetComponentInChildren<SpawnPointHandler>().node;
            var path = AStar.PathFromTo(spawnPoint, endNode);

            carsManager.SpawnCar(spawnPoint.nodePosition, path, endNode);
        }

        yield return null;
    }



}