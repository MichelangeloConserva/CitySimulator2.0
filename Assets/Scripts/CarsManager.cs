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
    private Quaternion rot;

    private bool spawn;

    public GameObject startPoint;
    public GameObject endPoint;



    void Start()
    {
        spawn = false;
        cars = new List<GameObject>();
    }

    public void SpawnCar(Quaternion rot)
    {
        var curCar = Instantiate(car, startNode.nodePosition + Vector3.up * 3, rot, garage.transform);
        cars.Add(curCar);
        Utils.SendVehicleFromTo(startNode, endNode, curCar);
    }

    void Update()
    {
        // Testing
        if (spawn)
        {
            //PickRandomTrip();
            GO();

            SpawnCar(rot);

            spawn = false;
        }
    }

    void GO()
    {
        startNode = startPoint.GetComponent<NodeHandler>().node;
        endNode = endPoint.GetComponent<NodeHandler>().node;
    }


    void PickRandomTrip()
    {
        var streetPoints = GameObject.FindGameObjectsWithTag("streetPoint");
        var start = streetPoints[(int)Random.Range(0, streetPoints.Length - 1)];
        rot = start.transform.rotation;
        startNode = start.GetComponent<NodeHandler>().node;
        endNode = streetPoints[(int)Random.Range(0, streetPoints.Length - 1)].GetComponent<NodeHandler>().node;
    }

    public void SpawnCar()
    {
        spawn = true;
    }


    void OnGUI()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(10, 10, 300, 100), string.Format("Number of cars: {0}", garage.transform.childCount), style);
    }


}
