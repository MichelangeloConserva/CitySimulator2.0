using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUCarsHandler : MonoBehaviour
{

    public GameObject car;

    [HideInInspector]
    public HUInitFamily huInitFamily;

    [HideInInspector]
    public NodeStreet spawnPoint;

    public CarsManager carsManager;

    public CityManagementTime cityManagementTime;


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
        if (dtWorkingLeaving[adult].Hour == cityManagementTime.realTime.Hour &
               (dtWorkingLeaving[adult].Minute > cityManagementTime.realTime.Minute && 
               dtWorkingLeaving[adult].Minute < cityManagementTime.realTime.Minute + 5))
        {
            adultsAtWork[adult] = true;




            var endNode = workingPlaces[adult].GetComponentInChildren<SpawnPointHandler>().node;
            spawnPoint = GetComponentInChildren<SpawnPointHandler>().node;


            var car =WorkerMoving(spawnPoint, endNode, transform.rotation);

            workingPlaces[adult].GetComponent<WHInit>().AddWorker(adult, huInitFamily.huEconomy, this, car);
        }

        yield return null;
    }

    public GameObject WorkerMoving(NodeStreet startNode, NodeStreet endNode, Quaternion rot)
    {
        var worker = Instantiate(car, startNode.nodePosition, rot, carsManager.transform);
        Utils.SendVehicleFromTo(startNode, endNode, worker);
        return worker;
    }




}
