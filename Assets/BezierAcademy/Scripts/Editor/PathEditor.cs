using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreatorAndSettings))]
public class PathEditor : Editor {

    //for displaying and editing the path

    PathCreatorAndSettings creator;
    PathProcedural Path
    {
        get
        {
            return creator.path;
        }
    }

    const float segmentSelectDistanceThreshold = .1f;
    int selectedSegmentIndex = -1;

    private void OnEnable() // quando l'editor viene abilitato
    {

        creator = (PathCreatorAndSettings)target;
        if (creator.path == null) // se non c'è nessun path
        {
            creator.CreatePath(Vector3.zero); //crea un nuovo path  CAMBIATO////////////////////////////////////////////////////////////////
        }
    }

    public override void OnInspectorGUI()
    {
     
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Create new"))
        {
            Undo.RecordObject(creator, "Create new");
            creator.CreatePath(Vector3.zero); // CAMBIATO //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        bool isClosed = GUILayout.Toggle(Path.IsClosed, "Closed");
        if (isClosed != Path.IsClosed)
        {
            Undo.RecordObject(creator, "Toggle closed");
            Path.IsClosed = isClosed;
        }

        bool autoSetControlPoints = GUILayout.Toggle(Path.AutoSetControlPoints, "Auto Set Control Points");
        if (autoSetControlPoints != Path.AutoSetControlPoints)
        {
            Undo.RecordObject(creator, "Toggle auto set controls");
            Path.AutoSetControlPoints = autoSetControlPoints;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }

    private void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        //mousePos = mousePos - creator.transform.position;
        mousePos.y = 0;

        /*if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.control) //crea un punto in più, crea un segmento di strada
        {
            if (selectedSegmentIndex != -1)
            {
                Undo.RecordObject(creator, "Split Segment");
                Path.SplitSegment(mousePos, selectedSegmentIndex);
            }
            else if (!Path.IsClosed)
            {
                Undo.RecordObject(creator, "Add Segment");
                Path.AddSegment(mousePos);
            }
        }*/
        
        /*if(guiEvent.type == EventType.MouseDown && guiEvent.button == 1)  //cancella un segmento
        {
            float minDstToAnchor = creator.anchorDiameter * .5f; // una threshold che gestisce la distanza minima per selezionare un punto
            int closestAnchorIndex = -1; //un indice invalido all'inizio

            for (int i = 0; i < Path.NumPoints; i += 3)
            {
                float dst = Vector3.Distance(mousePos, Path[i]); // un for che scorre tutti gli anchor e quando la distanza tra mouse e anchor è sotto la threshold cancella
                if (dst < minDstToAnchor)
                {
                    minDstToAnchor = dst;
                    closestAnchorIndex = i;
                }
            }

            if(closestAnchorIndex != -1) //se l'indice non è più invalido, cancella chiamando DeleteSegment()
            {
                Undo.RecordObject(creator, "Delete segment");
                Path.DeleteSegment(closestAnchorIndex);
            }
        }*/

        /*if (guiEvent.type == EventType.MouseMove)    //vede se è abbastnza vicino a un segmento
        {
            float minDstToSegment = segmentSelectDistanceThreshold;
            int newSelectedSegmentIndex = -1;

            for (int i = 0; i < Path.NumSegments; i++)
            {
                Vector3[] points = Path.GetPointsInSegment(i);
                float dst = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);
                if (dst < minDstToSegment)
                {
                    minDstToSegment = dst;
                    newSelectedSegmentIndex = i;
                }
            }

            if(newSelectedSegmentIndex != selectedSegmentIndex)
            {
                selectedSegmentIndex = newSelectedSegmentIndex;
                HandleUtility.Repaint();
            }
        }*/

        HandleUtility.AddDefaultControl(0);
    }

    void Draw()
    {
        for (int i = 0; i < Path.NumSegments; i++)
        {
            Vector3[] points = Path.GetPointsInSegment(i);
            if (creator.displayControlPoints)
            {
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Color segmentCol = (i == selectedSegmentIndex && Event.current.shift) ? Color.red : Color.green;
            Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentCol, null, 2);
            
        }

        Handles.color = Color.red;
        for (int i = 0; i < Path.NumPoints; i++) //per ogni punto che abbiamo vogliamo creare degli Handles che si possono muovere in libertà
        {
            if (i % 3 == 0 || creator.displayControlPoints)
            {
                Handles.color = (i % 3 == 0) ? creator.anchorCol : creator.controlCol;
                float handleSize = (i % 3 == 0) ? creator.anchorDiameter : creator.controlDiameter;
                Vector3 newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, handleSize, Vector3.zero, Handles.CylinderHandleCap);
                //Vector3 newPos = creator.cyloList[i].transform.position;
                newPos.y = 0;
                if (Path[i] != newPos) //se la posizione è cambiata
                {
                    Undo.RecordObject(creator, "Move point"); //facciamo ricordare all'editor il cambio, per poter usare l'Undo
                    //Path.MovePoint(i, newPos);
                }
            }
        }
    }

    /*private void OnEnable() // quando l'editor viene abilitato
    {
        creator = (PathCreatorAndSettings)target;
        if (creator.path == null) // se non c'è nessun path
        {
            creator.CreatePath(Vector3.zero); //crea un nuovo path  CAMBIATO////////////////////////////////////////////////////////////////
        }
    }*/

    /*public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Create new"))
        {
            Undo.RecordObject(creator, "Create new");
            creator.CreatePath(Vector3.zero); // CAMBIATO //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        bool isClosed = GUILayout.Toggle(Path.IsClosed, "Closed");
        if (isClosed != Path.IsClosed)
        {
            Undo.RecordObject(creator, "Toggle closed");
            Path.IsClosed = isClosed;
        }

        bool autoSetControlPoints = GUILayout.Toggle(Path.AutoSetControlPoints, "Auto Set Control Points");
        if (autoSetControlPoints != Path.AutoSetControlPoints)
        {
            Undo.RecordObject(creator, "Toggle auto set controls");
            Path.AutoSetControlPoints = autoSetControlPoints;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }*/
}
