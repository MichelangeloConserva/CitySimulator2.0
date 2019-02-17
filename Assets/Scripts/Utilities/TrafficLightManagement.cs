using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class TrafficLightManagement : MonoBehaviour
{


    private float timeForLightChange;

    private int numberOfTrafficLights;
    private Dictionary<GameObject,TrafficLightLights> precedentTrafficLightLights;


    // Start is called before the first frame update
    void Start()
    {
        timeForLightChange = Settings.timeForLightChange;

        numberOfTrafficLights = transform.childCount - 1;
        precedentTrafficLightLights = new Dictionary<GameObject, TrafficLightLights>();
        for (int i = 0; i < numberOfTrafficLights; i++)
            precedentTrafficLightLights[transform.GetChild(i).gameObject] = TrafficLightLights.yellow;


        StartCoroutine("AlternateTrafficLights");
    }
    /*
        luceVerde1_l1.gameObject.transform.parent.gameObject.transform.GetChild(3).GetComponent<TrafficLightSignaller>().curLight = 2;
     */


    IEnumerator AlternateTrafficLights() // TODO : refactor
    {
        // Starting condition
        int index = 0;

        while (true)
        {
            var increaseIndex = true;
            var halfTime = false;
            // if green lights are present then they need to be turned into yellow
            for (int i = 0; i < numberOfTrafficLights; i++)
            {
                var curTrafficLight = transform.GetChild(i).gameObject;

                if (i == index && precedentTrafficLightLights[curTrafficLight] == TrafficLightLights.green)
                {
                    LightChange(transform.GetChild(i).gameObject, TrafficLightLights.yellow);
                    halfTime = true;
                }
                else if (i == index)
                {
                    LightChange(transform.GetChild(i).gameObject, TrafficLightLights.green);
                    increaseIndex = false;
                }
                else
                    LightChange(transform.GetChild(i).gameObject, TrafficLightLights.red);
            }

            
            if(increaseIndex)
                index = (index + 1) % numberOfTrafficLights;


            if (halfTime)
                yield return new WaitForSeconds(timeForLightChange/2);
            else
                yield return new WaitForSeconds(timeForLightChange);
        }
    }

    /// <summary>
    /// Change the light of a trafficlight with the selected one
    /// </summary>
    /// <param name="trafficLight"></param>
    /// <param name="lightToLit"></param>
    private void LightChange(GameObject trafficLight, TrafficLightLights lightToLit)
    {
        precedentTrafficLightLights[trafficLight] = GetTrafficLightLightActive(trafficLight);

        for (int i=0; i< 3; i++)
        {
            if ( i == (int)lightToLit)
            {
                trafficLight.transform.GetChild(i).gameObject.SetActive(true);
                trafficLight.transform.GetChild(3).GetComponent<TrafficLightSignaller>().curLight = lightToLit;
            }
            else
                trafficLight.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private TrafficLightLights GetTrafficLightLightActive(GameObject trafficLight)
    {
        for (int i = 0; i < 3; i++)
        {
            if (trafficLight.transform.GetChild(i).gameObject.activeInHierarchy)
                return (TrafficLightLights)i;
        }
        return (TrafficLightLights)0;
    }




    /*
     
                for (int i = 0; i < numberOfTrafficLights; i++)
            {
                

                if (GetTrafficLightLightActive(curTrafficLight) == TrafficLightLights.red)
                {
                    if (precedentTrafficLightLights[curTrafficLight] == TrafficLightLights.red)
                    {
                        LightChange(curTrafficLight, TrafficLightLights.green);
                    }
                    else if (precedentTrafficLightLights[curTrafficLight] == TrafficLightLights.yellow)
                    {
                        precedentTrafficLightLights[curTrafficLight] = TrafficLightLights.red;
                    }
                }

                else if (GetTrafficLightLightActive(curTrafficLight) == TrafficLightLights.yellow)
                {
                    LightChange(curTrafficLight, TrafficLightLights.red);
                }

                else if (GetTrafficLightLightActive(curTrafficLight) == TrafficLightLights.green)
                {
                    LightChange(curTrafficLight, TrafficLightLights.yellow);

                }
            }
     
     */

}
