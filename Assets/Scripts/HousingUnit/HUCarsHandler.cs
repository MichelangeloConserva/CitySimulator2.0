using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUCarsHandler : MonoBehaviour
{

    [HideInInspector]
    public HUInitFamily huInitFamily;

    [HideInInspector]
    public Vector3 spawnPoint;

    public System.DateTime[] dtWorkingLeaving;

    public bool[] adultsAtWork;

    void Update()
    {
        if (dtWorkingLeaving != null)
            for (int adult = 0; adult < huInitFamily.numberOfAdultsComponents; adult++)
                if (!adultsAtWork[adult])
                    if (dtWorkingLeaving[adult].Hour == CityManagementTime.realTime.Hour &
                       (dtWorkingLeaving[adult].Minute > CityManagementTime.realTime.Minute - 1 && dtWorkingLeaving[adult].Minute > CityManagementTime.realTime.Minute + 1))
                        adultsAtWork[adult] = true;
    }

    public void SetGoingToWorkTime()
    {

    }




}
