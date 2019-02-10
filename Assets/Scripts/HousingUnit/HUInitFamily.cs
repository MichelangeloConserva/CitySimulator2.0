using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This file initialize all the family activities and settings.
/// 1. Number of components
/// 2. Number of car to be set in HU
/// 3. Wealth to be set in HUEconomy
/// </summary>
public class HUInitFamily : MonoBehaviour
{

    HUCarsHandler huCarsHandler;
    HUEconomy huEconomy;

    [Header("Family settings")]
    public int numberOfAdultsComponents;
    public int numberOfChildrenComponents;
    public List<float> possibleGoingToWorkHours;




    // Start is called before the first frame update
    void Start()
    {
        // Initliaze the family itself
        InitFamily();

        // Initialize the economy of the family
        StartCoroutine(InitEconomy());

        // Intialize the cars of the family and all their settings
        StartCoroutine(InitCarHandler());
    }


    private void InitFamily()
    {
        int[] possibleNumberOfAdultsComponents = new int[] {1,1,1,1,
                                                            2,2,2,2,2,2,2,2,2,2,2,2,2,
                                                            3,3,
                                                            4,4
                                                        };
        int[] possibleNumberOfChildrenComponents = new int[] {1,1,1,1,
                                                              2,2,2,2,2,2,2,2,2,2,2,2,2,
                                                              3,3,3,3,
                                                              4,4,4
                                                          };

        numberOfAdultsComponents   = possibleNumberOfAdultsComponents  [Random.Range(0, possibleNumberOfAdultsComponents.Length-1)];
        numberOfChildrenComponents = possibleNumberOfChildrenComponents[Random.Range(0, possibleNumberOfChildrenComponents.Length-1)];


        possibleGoingToWorkHours = new List<float> { 6f, 6.30f, 6.45f, 7f, 7.15f, 7.30f, 7.45f, 8, 8.15f, 8.30f };
    }

    private IEnumerator InitCarHandler()
    {
        // Waiting for the network to be updated
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // References linking
        huCarsHandler = GetComponent<HUCarsHandler>();
        huCarsHandler.huInitFamily = this;

        if (huCarsHandler.carsManager == null)
            huCarsHandler.carsManager = GameObject.Find("Manager").GetComponent<CarsManager>();



        // Setting spawn point for the cars
        huCarsHandler.spawnPoint = GetComponentInChildren<SpawnPointHandler>().node;



        // TODO : implements destinations, timing for spawning, type of car
        


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
            var possibleWorkingPlaces = GameObject.FindGameObjectsWithTag("workingPlace");
            var workingPlace = possibleWorkingPlaces[Random.Range(0, possibleWorkingPlaces.Length - 1)];
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
        var timeToGo = possibleGoingToWorkHours[Random.Range(0, possibleGoingToWorkHours.Count - 1)];
        var hours = (int)timeToGo; var minutes = (int)((timeToGo - hours) * 100);

        dt = dt.AddHours(hours); dt = dt.AddMinutes(minutes);

        huCarsHandler.dtWorkingLeaving[indexOfAdult] = dt;
        possibleGoingToWorkHours.Remove(timeToGo);
    }



    private IEnumerator InitEconomy()
    {
        // Waiting for the network to be updated
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();


        huEconomy = GetComponent<HUEconomy>();

        // TODO : Implements household economy mechanics
    }

}
