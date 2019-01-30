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

	
	void Update () {

    }


    public void SpawnCar(Vector3 startPos, List<Vector3> wayPoints)
    {
        Debug.Log("Spawned car");
        var curCar = Instantiate(car, startPos + Vector3.up, Quaternion.identity, garage.transform);
        curCar.GetComponent<CarAgent>().waypoints = wayPoints;
    }

}
