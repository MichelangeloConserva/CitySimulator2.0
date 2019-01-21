using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreatorAndSettings : MonoBehaviour {
    //for creating and holding a reference of the path

        [HideInInspector]
    public PathProcedural path;
    //public PlaymodeEditor playmode 
    
    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    public void CreatePath(Vector3 pos)
    {
        path = new PathProcedural(pos); //creiamo un path con il centro sul transform.position
    }

    /*private void Reset()
    {
        CreatePath();
    }*/
}
