using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadProceduralMeshCreator))]
public class RoadEditor : Editor
{

    RoadProceduralMeshCreator creator;

    private void OnSceneGUI()
    {
        if (creator.autoUpdate )//&& Event.current.type == EventType.Repaint)
        {
            creator.UpdateRoad();
            //divCreator.UpdateDiv();
        }
    }

    private void OnEnable()
    {
        creator = (RoadProceduralMeshCreator)target;
    }
}
