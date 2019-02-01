using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour {

    public GameObject car;
    public GameObject garage;

    public List<GameObject> cars;

    public RoadSpawn roadSpawn;
    public Network net;

    void Start()
    {
        cars = new List<GameObject>();
    }

    public void SpawnCar(Vector3 startPos, List<Vector3> wayPoints)
    {
        Debug.Log("Spawned car");
        var curCar = Instantiate(car, startPos + Vector3.up * 3, Quaternion.identity, garage.transform);
        curCar.transform.LookAt(wayPoints[1]);
        cars.Add(curCar);
        curCar.GetComponent<CarAgent>().waypoints = wayPoints;
    }

}
