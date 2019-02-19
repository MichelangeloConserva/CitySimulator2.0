using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAndReference : MonoBehaviour
{
    RoadSpawn roadSpawn;
    Button myself;
    
    /// <summary>
    /// Gets reference to itself and to whatever it needs to interact with, then sends the clicked message
    /// </summary>
    private void Awake()
    {
        myself = GetComponent<Button>();
        roadSpawn = FindObjectOfType<RoadSpawn>();
        myself.onClick.AddListener(Clicked);
    }

    void Clicked()
    {
        roadSpawn.BuildButtonResponse(this.gameObject.name);
    }


}