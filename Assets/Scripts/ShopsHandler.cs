using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopsHandler : MonoBehaviour
{



    public IEnumerator ShopperResender(int hours, NodeStreet backNode,GameObject actualWorkerCar, GameObject workerCarModel, int minutes = 0)
    {
        while(Vector3.Distance(Utils.Down(transform.position), actualWorkerCar.transform.position) > 5)
            yield return new WaitForFixedUpdate();

        yield return new WaitForSeconds( (hours * 3600 + minutes * 60) / Settings.timeMultiplyer);

        var startNode = GetComponentInChildren<SpawnPointHandler>().node;
        var car = Instantiate(workerCarModel, startNode.nodePosition, transform.rotation);
        Utils.SendVehicleFromTo(startNode, backNode, car);

    }


}
