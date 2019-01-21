using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManagerBezier : MonoBehaviour {

    public GameObject car;
    public GameObject garage;

    public List<GameObject> cars;


    void Start()
    {
        cars = new List<GameObject>();
    }

	
    public void SpawnCar(Vector3 startPos, List<Vector3> wayPoints)
    {
        Debug.Log("Spawned car");
        var curCar = Instantiate(car, startPos + Vector3.up, Quaternion.identity, garage.transform);
        curCar.GetComponent<CarAgent>().waypoints = wayPoints;
    }

}
