using System.Collections;
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


        StartCoroutine(SenderCoroutine());
    }
   
    private IEnumerator SenderCoroutine()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            var destinations = new List<NodeStreet>();
            foreach (int i in UniqueRandom(2, destinationPlaces.Count))
                destinations.Add(destinationPlaces[i].GetComponentInChildren<SpawnPointHandler>().node);

            SendTruck(destinations);

            yield return new WaitForSeconds(hoursInterval * 3600 / Settings.timeMultiplyer);
        }
    }




    private void SendTruck(List<NodeStreet> destinations)
    {
        var truck = Instantiate(garbageTruck, transform.position+ Vector3.up, transform.rotation);

        truck.GetComponent<TruckAIController>().destinations = destinations;
        truck.GetComponent<TruckAIController>().waypoints = new List<Vector3> { Vector3.zero };

        StartCoroutine(truck.GetComponent<TruckAIController>().Recalculating());

    }


    private int[] UniqueRandom(int uniques, int max)
    {
        var numbers = new List<int>(max);
        for (int i = 0; i < max; i++)
            numbers.Add(i);

        var randomNumbers = new int[uniques];
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            var thisNumber = Random.Range(0, numbers.Count);

            randomNumbers[i] = numbers[thisNumber];
            numbers.RemoveAt(thisNumber);
        }

        return randomNumbers;
    }

}
