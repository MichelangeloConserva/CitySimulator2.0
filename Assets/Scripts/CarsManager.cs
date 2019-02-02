using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour {

    public GameObject car;
    public GameObject garage;

    public List<GameObject> cars;

    public RoadSpawn roadSpawn;
    public Network net;


    private NodeStreet startNode;
    private NodeStreet endNode;


    private bool spawn;


    void Start()
    {
        spawn = false;
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


    void Update()
    {
        // Testing
        if (spawn)
        {

            PickRandomTrip();

            var pathFinder = new AStar(startNode, endNode);
            var found = pathFinder.PathFinder();
            var path = new List<Vector3>();
            foreach (NodeStreet n in pathFinder.path)
                path.Add(n.nodePosition);

            Debug.Log(path.Count);

            if (path.Count > 1)
                SpawnCar(startNode.nodePosition, path);

            spawn = false;
        }
    }

    void PickRandomTrip()
    {
        var streetPoints = GameObject.FindGameObjectsWithTag("streetPoint");
        startNode = streetPoints[(int)Random.Range(0, streetPoints.Length - 1)].GetComponent<NodeHandler>().node;
        endNode = streetPoints[(int)Random.Range(0, streetPoints.Length - 1)].GetComponent<NodeHandler>().node;
    }

    public void SpawnCar()
    {
        spawn = true;
    }

}
