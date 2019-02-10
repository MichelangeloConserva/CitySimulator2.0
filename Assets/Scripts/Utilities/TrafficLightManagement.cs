using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManagement : MonoBehaviour
{

    List<Transform> lane_1 = new List<Transform>();
    List<Transform> lane_2 = new List<Transform>();
    //inizializzo referenze luci
    Transform luceVerde1_l1;
    Transform luceVerde2_l1;
    Transform luceVerde1_l2;
    Transform luceVerde2_l2;
    Transform luceGialla1_l1;
    Transform luceGialla2_l1;
    Transform luceGialla1_l2;
    Transform luceGialla2_l2;
    Transform luceRossa1_l1;
    Transform luceRossa2_l1;
    Transform luceRossa1_l2;
    Transform luceRossa2_l2;

    public float timeForLightChange;

    // Start is called before the first frame update
    void Start()
    {
        //c'è sempre almeno una lane
        lane_1.Add(gameObject.transform.Find("Semaforo1_L1"));
        luceVerde1_l1 = lane_1[0].gameObject.transform.Find("Verde");  //referenze per le luci
        luceGialla1_l1 = lane_1[0].gameObject.transform.Find("Giallo");
        luceRossa1_l1 = lane_1[0].gameObject.transform.Find("Rosso");
        if (gameObject.transform.Find("Semaforo2_L1"))
        {
            lane_1.Add(gameObject.transform.Find("Semaforo2_L1"));
            luceVerde2_l1 = lane_1[1].gameObject.transform.Find("Verde");
            luceGialla2_l1 = lane_1[1].gameObject.transform.Find("Giallo");
            luceRossa2_l1 = lane_1[1].gameObject.transform.Find("Rosso");
        }

        lane_2.Add(gameObject.transform.Find("Semaforo1_L2"));
        luceVerde1_l2 = lane_2[0].gameObject.transform.Find("Verde");
        luceGialla1_l2 = lane_2[0].gameObject.transform.Find("Giallo");
        luceRossa1_l2 = lane_2[0].gameObject.transform.Find("Rosso");
        if (gameObject.transform.Find("Semaforo2_L2"))
        {
            lane_2.Add(gameObject.transform.Find("Semaforo2_L2"));
            luceVerde2_l2 = lane_2[1].gameObject.transform.Find("Verde");
            luceGialla2_l2 = lane_2[1].gameObject.transform.Find("Giallo");
            luceRossa2_l2 = lane_2[1].gameObject.transform.Find("Rosso");
        }

        StartCoroutine("AlternateTrafficLights");

        /*Debug.Log("la lane 1 contiene " + lane_1.Count + " elementi");
        Debug.Log("la lane 2 contiene " + lane_2.Count + " elementi");*/
        
    }

    IEnumerator AlternateTrafficLights()
    {
        if (lane_1.Count == 2 && lane_2.Count == 2) 
            while (true)
            {
                //luce verde per lane 1 e luce rossa per lane 2
                luceGialla1_l2.gameObject.SetActive(false);
                luceGialla2_l2.gameObject.SetActive(false);

                luceVerde1_l1.gameObject.SetActive(true);
                luceVerde2_l1.gameObject.SetActive(true);

                luceRossa1_l2.gameObject.SetActive(true);
                luceRossa2_l2.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce gialla per lane 1 e luce rossa per lane 2

                luceVerde1_l1.gameObject.SetActive(false);
                luceVerde2_l1.gameObject.SetActive(false);

                luceGialla1_l1.gameObject.SetActive(true);
                luceGialla2_l1.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce rossa per lane 1 e luce verde per lane 2

                luceGialla1_l1.gameObject.SetActive(false);
                luceGialla2_l1.gameObject.SetActive(false);

                luceRossa1_l1.gameObject.SetActive(true);
                luceRossa2_l1.gameObject.SetActive(true);

                luceRossa1_l2.gameObject.SetActive(false);
                luceRossa2_l2.gameObject.SetActive(false);

                luceVerde1_l2.gameObject.SetActive(true);
                luceVerde2_l2.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce rossa per lane 1 e luce gialla per lane 2

                luceGialla1_l2.gameObject.SetActive(true);
                luceGialla2_l2.gameObject.SetActive(true);

                luceVerde1_l2.gameObject.SetActive(false);
                luceVerde2_l2.gameObject.SetActive(false);

                yield return new WaitForSeconds(timeForLightChange);
            }

        else if (lane_1.Count == 1 && lane_2.Count == 2)
            while (true)
            {
                //luce verde per lane 1 e luce rossa per lane 2
                luceGialla1_l2.gameObject.SetActive(false);
                luceGialla2_l2.gameObject.SetActive(false);

                luceVerde1_l1.gameObject.SetActive(true);               

                luceRossa1_l2.gameObject.SetActive(true);
                luceRossa2_l2.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce gialla per lane 1 e luce rossa per lane 2

                luceVerde1_l1.gameObject.SetActive(false);

                luceGialla1_l1.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce rossa per lane 1 e luce verde per lane 2

                luceGialla1_l1.gameObject.SetActive(false);

                luceRossa1_l1.gameObject.SetActive(true);

                luceRossa1_l2.gameObject.SetActive(false);
                luceRossa2_l2.gameObject.SetActive(false);

                luceVerde1_l2.gameObject.SetActive(true);
                luceVerde2_l2.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce rossa per lane 1 e luce gialla per lane 2

                luceGialla1_l2.gameObject.SetActive(true);
                luceGialla2_l2.gameObject.SetActive(true);

                luceVerde1_l2.gameObject.SetActive(false);
                luceVerde2_l2.gameObject.SetActive(false);

                yield return new WaitForSeconds(timeForLightChange);
            }

        else if (lane_1.Count == 2 && lane_2.Count == 1)
            while (true)
            {
                //luce verde per lane 1 e luce rossa per lane 2
                luceGialla1_l2.gameObject.SetActive(false);

                luceVerde1_l1.gameObject.SetActive(true);
                luceVerde2_l1.gameObject.SetActive(true);

                luceRossa1_l2.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce gialla per lane 1 e luce rossa per lane 2

                luceVerde1_l1.gameObject.SetActive(false);
                luceVerde2_l1.gameObject.SetActive(false);

                luceGialla1_l1.gameObject.SetActive(true);
                luceGialla2_l1.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce rossa per lane 1 e luce verde per lane 2

                luceGialla1_l1.gameObject.SetActive(false);
                luceGialla2_l1.gameObject.SetActive(false);

                luceRossa1_l1.gameObject.SetActive(true);
                luceRossa2_l1.gameObject.SetActive(true);

                luceRossa1_l2.gameObject.SetActive(false);

                luceVerde1_l2.gameObject.SetActive(true);

                yield return new WaitForSeconds(timeForLightChange);

                //luce rossa per lane 1 e luce gialla per lane 2

                luceGialla1_l2.gameObject.SetActive(true);

                luceVerde1_l2.gameObject.SetActive(false);

                yield return new WaitForSeconds(timeForLightChange);
            }


        yield return null;
    }

}
