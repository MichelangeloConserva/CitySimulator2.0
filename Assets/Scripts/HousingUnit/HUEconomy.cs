using System.Collections.Generic;
using UnityEngine;
using static Utils;


public class HUEconomy : MonoBehaviour
{
    [HideInInspector]
    public HUInitFamily huInitFamily;

    public GameObject[] supermarketNearby;



    [Header("Costs")]
    public float[] possibleBreakfastCostPerPerson;
    public float[] possibleLaunchCostPerPerson;
    public float[] possibleDinnerCostPerPerson;

    [Header("FamilyInfo")]
    public int numberOfPeopleInTheHouse;
    public float savings;
    public float savingsMorning;
    public float savingsNight;
    public List<float> earningHistory;
    public int daysWithoutGoingToSupermarketMax;


    public int adultsAtHome;

    private int daysWithoutGoingToSupermarket;



    public void Initialize()
    {
        daysWithoutGoingToSupermarketMax = 5;
        daysWithoutGoingToSupermarket = 4;

        numberOfPeopleInTheHouse = huInitFamily.numberOfAdultsComponents + huInitFamily.numberOfChildrenComponents;

        possibleBreakfastCostPerPerson = new float[] { 3.5f, 4f, 5 };
        possibleLaunchCostPerPerson = new float[] { 10f, 14f, 15 };
        possibleDinnerCostPerPerson = new float[] { 13.5f, 14f, 15, 25f };

    }


    /// <summary>
    /// Called when the parte of the day changes
    /// </summary>
    /// <param name="dayTime"></param>
    public void DayTimeCostsUpdate(DayTime dayTime)
    {

        switch (dayTime)
        {
            case DayTime.earlyMorning:
                savingsMorning = savings;
                break;

            case DayTime.breakfast:
                SpendMoney(numberOfPeopleInTheHouse, RandomFromArray(ref possibleBreakfastCostPerPerson));
                break;


            case DayTime.launch:
                SpendMoney(numberOfPeopleInTheHouse, RandomFromArray(ref possibleLaunchCostPerPerson));
                break;

            case DayTime.afternoon:

                if (NumOfAdultsAtHome() > 0 && (float)(daysWithoutGoingToSupermarket / daysWithoutGoingToSupermarketMax) >= Random.Range(0, 1))
                {
                    var curMarket = RandomFromArray(ref supermarketNearby);
                    var endNode = curMarket.GetComponentInChildren<SpawnPointHandler>().node;
                    var car = Instantiate(huInitFamily.car, huInitFamily.spawnPoint.nodePosition, transform.rotation);

                    SendVehicleFromTo(huInitFamily.spawnPoint, endNode, car);


                    StartCoroutine(curMarket.GetComponent<ShopsHandler>().ShopperResender(1, huInitFamily.spawnPoint,car, huInitFamily.car));

                    daysWithoutGoingToSupermarket = 0;
                }
                else
                {
                    daysWithoutGoingToSupermarket++;
                }
                break;

            case DayTime.dinner:
                SpendMoney(numberOfPeopleInTheHouse, RandomFromArray(ref possibleDinnerCostPerPerson));
                break;

            case DayTime.evening:
                NightCostUpdate();
                savingsNight = savings;
                earningHistory.Add(savingsMorning - savingsNight);
                break;
        }
    }

    private int NumOfAdultsAtHome()
    {
        int numOfAdultsAtHome = 0;
        foreach (bool b in huInitFamily.huCarsHandler.adultsAtWork)
            if (!b)
            {
                numOfAdultsAtHome += 1;
            }
        return numOfAdultsAtHome;
    }


    private void NightCostUpdate()
    {
        // TODO : add costs


        // Fuel cost for the day
        SpendMoney(huInitFamily.numberOfAdultsComponents, 5);

        // Taxes
        SpendMoney(huInitFamily.numberOfAdultsComponents, 5);

    }

    /// <summary>
    /// Spend cost money for numberOfPeople
    /// </summary>
    /// <param name="numberOfPeople"></param>
    /// <param name="cost"></param>
    private void SpendMoney(int numberOfPeople, float cost)
    {
        savings -= cost * numberOfPeople;
    }

    



}