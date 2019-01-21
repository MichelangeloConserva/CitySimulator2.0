using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCollidingScript : MonoBehaviour {

    public bool isColliding = false;

    public GameObject otherRoadRef;
    public GameObject otherSphere;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "sferetta")
        {
            otherSphere = col.gameObject;
            otherSphere.GetComponent<SphereCollider>().enabled = false;
            isColliding = true;
            otherRoadRef = col.gameObject.transform.parent.gameObject;
            otherRoadRef.GetComponentInParent<RoadSpawn>().CrossRoadFinder(otherRoadRef, this.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "sferetta")
            isColliding = false;
    }

   

}
