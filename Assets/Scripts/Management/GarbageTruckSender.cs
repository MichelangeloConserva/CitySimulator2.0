﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageTruckSender : MonoBehaviour
{
    public GameObject garbageTruck;

    public List<GameObject> destinationPlaces;


    public int hoursInterval;

    private NodeStreet spawnPoint;


    void Start()
    {
        destinationPlaces = new List<GameObject>();


        spawnPoint = transform.GetChild(0).GetComponent<SpawnPointHandler>().node;

        // Find all the places where to send truck
        foreach (GameObject house in GameObject.FindGameObjectsWithTag("housingUnit"))
            destinationPlaces.Add(house);

        StartCoroutine(ContinousSenderProcedure());
    }

    /// <summary>
    /// Sends a garbage collector in random HU every hoursInterval hours
    /// </summary>
    /// <returns></returns>
    private IEnumerator ContinousSenderProcedure()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            // Adding destinations
            var destinations = new List<NodeStreet>();
            foreach (int i in Utils.UniqueRandom(2, destinationPlaces.Count))
                destinations.Add(destinationPlaces[i].GetComponentInChildren<SpawnPointHandler>().node);
            destinations.Add(GetComponentInChildren<SpawnPointHandler>().node);

            // Sending the truck
            SendTruck(destinations);


            yield return new WaitForSeconds(hoursInterval * 3600 / Settings.timeMultiplyer);
        }
        
    }

    /// <summary>
    /// Instantiate the truck and send it to the predefined destinations
    /// </summary>
    /// <param name="destinations"></param>
    private void SendTruck(List<NodeStreet> destinations)
    {
        var truck = Instantiate(garbageTruck, transform.position+ Vector3.up, transform.rotation);

        truck.GetComponent<TruckAIController>().destinations = destinations;
        truck.GetComponent<TruckAIController>().waypoints = new List<Vector3> { Vector3.zero };

        StartCoroutine(truck.GetComponent<TruckAIController>().Recalculating());
    }


}