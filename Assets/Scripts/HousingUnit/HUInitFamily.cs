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
    }

    private IEnumerator InitCarHandler()
    {
        // Waiting for the network to be updated
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // References linking
        huCarsHandler = GetComponent<HUCarsHandler>();
        huCarsHandler.huInitFamily = this;


        // Setting spawn point for the cars
        huCarsHandler.spawnPoint = GetComponentInChildren<SpawnPointHandler>().node.nodePosition;



        // TODO : implements destinations, timing for spawning, type of car
        float[] possibleGoingToWorkHours = new float[] { 6f, 6.30f, 6.45f, 7f, 7.15f, 7.30f, 7.45f, 8, 8.15f, 8.30f };


        // Initializing the variables of the CarsHandlers
        huCarsHandler.dtWorkingLeaving = new System.DateTime[numberOfAdultsComponents];
        huCarsHandler.adultsAtWork = new bool[numberOfAdultsComponents];
        for (int i = 0; i < numberOfAdultsComponents; i++)
            huCarsHandler.adultsAtWork[i] = false;


        for (int i=0; i<numberOfAdultsComponents; i++)
        {
            System.DateTime dt = new System.DateTime();
            var timeToGo = possibleGoingToWorkHours[Random.Range(0, possibleGoingToWorkHours.Length - 1)];
            var hours = (int)timeToGo;
            var minutes = (int)((timeToGo - hours) * 100);
            dt = dt.AddHours(hours);
            dt = dt.AddMinutes(minutes);
            huCarsHandler.dtWorkingLeaving[i] = dt;
        }



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
