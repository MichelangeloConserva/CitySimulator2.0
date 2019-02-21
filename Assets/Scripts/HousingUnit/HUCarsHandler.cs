using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUCarsHandler : MonoBehaviour
{

    public GameObject car;

    [HideInInspector]
    public HUInitFamily huInitFamily;

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





            var endNode = workingPlaces[adult].GetComponent<WHInit>().GetspawnPoint();

            Utils.DrawDebugArrow(workingPlaces[adult].GetComponent<WHInit>().GetspawnPoint().nodePosition, 
                Vector3.up * 3, 
                Color.black, Mathf.Infinity);

            var spawnPoint = huInitFamily.GetspawnPoint();

            
            var car = WorkerMoving(spawnPoint, endNode, transform.rotation);
            if (car != null)
            {
                workingPlaces[adult].GetComponent<WHInit>().AddWorker(adult, huInitFamily.huEconomy, this, car);
                adultsAtWork[adult] = true;
            }
            else
            {
                yield return null;
            }

        }

        yield return null;
    }

    public GameObject WorkerMoving(NodeStreet startNode, NodeStreet endNode, Quaternion rot)
    {

        if (startNode == null || endNode == null)
        {
            return null;
        }
        else
        {
            var worker = Instantiate(car, startNode.nodePosition, rot, carsManager.garage.transform);
            StartCoroutine(SendWorker(startNode, endNode, worker));
   
            return worker;
        }
    }

    private IEnumerator SendWorker(NodeStreet startNode,NodeStreet endNode,GameObject worker)
    {
        yield return new WaitForFixedUpdate();
        Utils.SendVehicleFromTo(startNode, endNode, worker);
    }


}
