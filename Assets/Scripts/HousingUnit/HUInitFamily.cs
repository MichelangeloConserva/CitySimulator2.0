﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utils;

/// <summary>
/// This file initialize all the family activities and settings.
/// 1. Number of components
/// 2. Number of car to be set in HU
/// 3. Wealth to be set in HUEconomy
/// </summary>
public class HUInitFamily : MonoBehaviour
{
    [Header("Objects to be used")]
    [Space]
    public GameObject car;

    [Header("References")]
    [Space]
    public SingleCity sc;
    public HUGeneralManager huGeneralManager;
    public HUCarsHandler huCarsHandler;
    public HUEconomy huEconomy;

    [Header("Family settings")]
    [Space]
    public int numberOfAdultsComponents;
    public int numberOfChildrenComponents;

    
    private float[] possibleGoingToWorkHours;
    private float[] possibleInitialSavings;
    private int[] possibleNumberOfAdultsComponents;
    private int[] possibleNumberOfChildrenComponents;
    private GameObject[] possibleWorkingPlaces;
    private GameObject[] supermarkets;


    public NodeStreet GetspawnPoint() => GetComponentInChildren<SpawnPointHandler>().node;



    // Start is called before the first frame update
    void Start()
    {
        //huGeneralManager = GetComponentInParent<HUGeneralManager>();
        huGeneralManager.HUs.Add(this);

        // Initliaze the family itself
        InitFamily();

        // Intialize the cars of the family and all their settings
        huCarsHandler = GetComponent<HUCarsHandler>();
        StartCoroutine(InitCarHandler());

        // Initialize the economy of the family
        huEconomy = GetComponent<HUEconomy>();
        InitEconomy();
    }

    /// <summary>
    /// Defining settings
    /// </summary>
    private void InitFamily()
    {

        var workplacesNearby = sc.GetWorkplaces().workplaces;
        var housesNearby = sc.GetHuUnits().huUnits;
        var marketsNearby = sc.GetMarketplaces().marketplaces;

        // Family
        possibleNumberOfAdultsComponents = new int[] {1,1,1,1,
                                                            2,2,2,2,2,2,2,2,2,2,2,2,2,
                                                            3,3,
                                                            4,4
                                                        };
        possibleNumberOfChildrenComponents = new int[] {1,1,1,1,
                                                              2,2,2,2,2,2,2,2,2,2,2,2,2,
                                                              3,3,3,3,
                                                              4,4,4
                                                          };

        // Economy
        possibleInitialSavings = new float[] { 1000f, 2000f, 3000f };
        possibleWorkingPlaces = workplacesNearby.ToArray();
        supermarkets = marketsNearby.ToArray();

        // Cars
        possibleGoingToWorkHours = new float[] { 6.15f, 6.30f, 6.45f, 7f }; // { 6f, 6.30f, 6.45f, 7f, 7.15f, 7.30f, 7.45f, 8, 8.15f, 8.30f };

        // Setting references
        numberOfAdultsComponents = RandomFromArray(ref possibleNumberOfAdultsComponents);
        numberOfChildrenComponents = RandomFromArray(ref possibleNumberOfChildrenComponents);
        huGeneralManager.numAdults += numberOfAdultsComponents;

    }

    private IEnumerator InitCarHandler()
    {
        // Waiting for the network to be updated
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // References linking
        huCarsHandler.huInitFamily = this;

        if (huCarsHandler.carsManager == null)
            huCarsHandler.carsManager = GetComponentInParent<CarsManager>();
        if (huCarsHandler.cityManagementTime == null)
            huCarsHandler.cityManagementTime = GameObject.Find("GlobalManager").GetComponent<CityManagementTime>();


        huCarsHandler.car = car; // TODO : add the possibility of more cars prefabs


        // Initializing CarHandlers variables for the adults
        huCarsHandler.dtWorkingLeaving = new System.DateTime[numberOfAdultsComponents];
        huCarsHandler.adultsAtWork = new bool[numberOfAdultsComponents];
        huCarsHandler.workingPlaces = new GameObject[numberOfAdultsComponents];


        for (int i=0; i<numberOfAdultsComponents; i++)
        {
            // None of the adults is at work
            huCarsHandler.adultsAtWork[i] = false;

            // Setting time for the adults to leave
            SetWorkTime(i);

            // Setting the working place
            var workingPlace = RandomFromArray(ref possibleWorkingPlaces);
            huCarsHandler.workingPlaces[i] = workingPlace;

        }
    }

    /// <summary>
    /// From a prefedefined list of possibile time we choose a time for the adult
    /// </summary>
    /// <param name="indexOfAdult"></param>
    private void SetWorkTime(int indexOfAdult)
    {
        System.DateTime dt = new System.DateTime();
        var timeToGo = RandomFromArray(ref possibleGoingToWorkHours);
        var hours = (int)timeToGo; var minutes = (int)((timeToGo - hours) * 100 + Random.Range(-5, 5)); // a little bit of noise

        dt = dt.AddHours(hours); dt = dt.AddMinutes(minutes);

        huCarsHandler.dtWorkingLeaving[indexOfAdult] = dt;

        // Removing the selected work hour
        var possibleGoingToWorkHours_ = possibleGoingToWorkHours.ToList();
        possibleGoingToWorkHours_.Remove(timeToGo);
        possibleGoingToWorkHours = possibleGoingToWorkHours_.ToArray();
    }



    private void InitEconomy()
    {
        // Waiting for the network to be updated


        huEconomy.huInitFamily = this;

        huEconomy.savingsMorning = RandomFromArray(ref possibleInitialSavings);
        huEconomy.savings += huEconomy.savingsMorning;

        huEconomy.supermarketNearby = supermarkets;


        huEconomy.Initialize();
        
        // TODO : Implements household economy mechanics
    }

}
