using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HUDRendering hud;

    public List<GameObject> properties;

    public float money;

    void Start()
    {
        money = 10000;
    }

    public void AcquireProperty(float cost, GameObject property)
    {

    }


}
