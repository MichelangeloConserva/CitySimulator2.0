using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecking : MonoBehaviour {


    public bool isColliding;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Terrain" && other.tag != "Trace")
            isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
