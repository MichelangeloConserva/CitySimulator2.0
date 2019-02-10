using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour {

    public GameObject car;
    public GameObject garage;

    public List<GameObject> cars;

    public RoadSpawn roadSpawn;

    private NodeStreet startNode;
    private NodeStreet endNode;

    private bool spawn;


    void Start()
    {
        spawn = false;
        cars = new List<GameObject>();
    }

    public void SpawnCar(Vector3 startPos, List<Vector3> wayPoints, NodeStreet lastNode)
    {
        var curCar = Instantiate(car, startPos + Vector3.up * 3, Quaternion.identity, garage.transform);
        curCar.transform.LookAt(wayPoints[1]);
        cars.Add(curCar);
        curCar.GetComponent<CarAgent>().waypoints = wayPoints;
        curCar.GetComponent<CarAgent>().endNode = lastNode;
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

            if (path.Count > 1)
                SpawnCar(startNode.nodePosition, path, endNode);

            spawn = false;
        }
    }

    void PickRandomTrip()
    {
        var streetPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        startNode = streetPoints[0].GetComponent<SpawnPointHandler>().node;
        endNode = streetPoints[1].GetComponent<SpawnPointHandler>().node;
    }

    public void SpawnCar()
    {
        spawn = true;
    }


    void OnGUI()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.red;
        GUI.Label(new Rect(10, 10, 300, 100), string.Format("Number of cars: {0}", garage.transform.childCount), style);
    }


}
