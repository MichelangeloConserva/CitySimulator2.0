using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{


    private CarsManager carsManager;


    private NodeStreet startNode;
    private NodeStreet endNode;


    public float waitUntilNextSpawn;
    
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;

        carsManager = GetComponent<CarsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= waitUntilNextSpawn)
        {
            timePassed = 0;

            PickRandomTrip();

            var pathFinder = new AStar(startNode, endNode);
            var found = pathFinder.PathFinder();
            var path = new List<Vector3>();
            foreach (NodeStreet n in pathFinder.path)
                path.Add(n.nodePosition);

            //tripPlanner.SetPositions(new Vector3[] { });
            //foreach (Vector3 v in path)
            //    tripPlanner.SetPosition(++tripPlanner.positionCount - 1, v + Vector3.up * 2f);

            Debug.Log(path.Count);

            if (path.Count > 10)
                carsManager.SpawnCar(startNode.nodePosition, path);
        }
        

    }

    void PickRandomTrip()
    {
        var streetPoints = GameObject.FindGameObjectsWithTag("streetPoint");
        startNode = streetPoints[(int)Random.Range(0, streetPoints.Length - 1)].GetComponent<NodeHandler>().node;
        endNode = streetPoints[(int)Random.Range(0, streetPoints.Length - 1)].GetComponent<NodeHandler>().node;
    }

}
