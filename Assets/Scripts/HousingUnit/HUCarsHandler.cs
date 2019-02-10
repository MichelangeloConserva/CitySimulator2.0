using System.Collections;
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
                    if (dtWorkingLeaving[adult].Hour == CityManagementTime.realTime.Hour &
                       (dtWorkingLeaving[adult].Minute > CityManagementTime.realTime.Minute  && dtWorkingLeaving[adult].Minute < CityManagementTime.realTime.Minute + 5))
                    {

                        adultsAtWork[adult] = true;

                        // pathfinding
                        var endNode = workingPlaces[adult].GetComponentInChildren<SpawnPointHandler>().node;
                        var pathFinder = new AStar(spawnPoint, endNode);
                        var found = pathFinder.PathFinder();
                        var path = new List<Vector3>();
                        foreach (NodeStreet n in pathFinder.path)
                            path.Add(n.nodePosition);

                        carsManager.SpawnCar(spawnPoint.nodePosition, path, endNode);
                    }
    }

    public void SetGoingToWorkTime()
    {

    }




}
