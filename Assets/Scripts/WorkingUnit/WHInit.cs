using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHInit : MonoBehaviour
{

    public NodeStreet GetspawnPoint() => GetComponentInChildren<SpawnPointHandler>().node;


    public float retributionPerHour;

    public List<WHWorker> workers;

    public int actualWorker;

    private int[] possibleHoursAtWork;

    void Start()
    {
        actualWorker = 0;

        possibleHoursAtWork = new int[] { 4, 6, 8 };

        workers = new List<WHWorker>();

        var possibleRetributionPerHour = new List<float> { 6f, 6.30f, 6.45f, 7f, 7.15f, 7.30f, 7.45f, 8, 8.15f, 8.30f };
        retributionPerHour = possibleRetributionPerHour[Random.Range(0, possibleRetributionPerHour.Count - 1)];
    }

    void Update()
    {
        actualWorker = workers.Count;
    }

    public void AddWorker(int adultIndex, HUEconomy huE, HUCarsHandler huC, GameObject workerCar)
    {
        int workingHours = possibleHoursAtWork[Random.Range(0, possibleHoursAtWork.Length - 1)];
        var spawnPoint = gameObject.GetComponentInChildren<SpawnPointHandler>().node;
        var worker = new WHWorker(adultIndex, huE, huC, System.DateTime.Now, workingHours, spawnPoint,transform.rotation);
        workers.Add(worker);
        StartCoroutine(WorkingCoroutine(worker,workerCar));
    }

    /// <summary>
    /// Start the coroutine which takes care of all the process of working of the worker
    /// </summary>
    /// <param name="worker"></param>
    /// <returns></returns>
    private IEnumerator WorkingCoroutine(WHWorker worker, GameObject actualWorkerCar)
    {
        while (Vector3.Distance(Utils.Down(transform.position), actualWorkerCar.transform.position) > 20)
            yield return new WaitForFixedUpdate();

        for (int i=0; i<worker.workingHours; i++)
        {
            yield return new WaitForSeconds(3600 / Settings.timeMultiplyer);
            worker.EarnMoney(retributionPerHour);
        }
       
        worker.GoHome();
        workers.Remove(worker);
    }





}
