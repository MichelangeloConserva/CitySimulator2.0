using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCity : MonoBehaviour
{

    /// <summary>
    /// A class containg the coordinates of the boundaries of the city
    /// and methods to show them
    /// </summary>
    public class Boundaries
    {
        public float xMin { get; set; }
        public float xMax { get; set; }
        public float zMin { get; set; }
        public float zMax { get; set; }

        public void SetBoundaries(Transform startPoint)
        {
            if (startPoint.childCount == 0)
            {
                var x = startPoint.position.x; var z = startPoint.position.z;

                if (x < xMin)
                    xMin = x;
                if (x > xMax)
                    xMax = x;
                if (z < zMin)
                    zMin = z;
                if (z > zMax)
                    zMax = z;
            }
            else
                for (int i = 0; i < startPoint.childCount; i++)
                    SetBoundaries(startPoint.GetChild(i).transform);
        }


        // TODO : add methods to show interesting things about boundaries
        public void showBoundaries()
        {

        }

        public List<Vector3> pointOnGridInside()
        {
            var grid = FindObjectOfType<GridProperties>();
            List<Vector3> pointsInside = new List<Vector3>();

            Vector3 frontRight = Utils.GetNearestPointOnGrid(new Vector3(xMax, 0, zMax));
            Vector3 frontLeft  = Utils.GetNearestPointOnGrid(new Vector3(xMax, 0, zMin));
            Vector3 rearRight  = Utils.GetNearestPointOnGrid(new Vector3(xMin, 0, zMax));
            Vector3 rearLeft   = Utils.GetNearestPointOnGrid(new Vector3(xMin, 0, zMin));
            
            for (float z = zMin; z<zMax; z+=grid.gridSize)
                for (float x = xMin; x < xMax; x += grid.gridSize)
                    pointsInside.Add(Utils.GetNearestPointOnGrid(new Vector3(x, 0, z)));
            return pointsInside;
        }
    }

    /// <summary>
    /// Class which holds the workplaces present in the city
    /// it contains methods for showing them
    /// </summary>
    public class Workplaces
    {
        public List<GameObject> workplaces;

        public Workplaces(Transform startPoint)
        {
            workplaces = new List<GameObject>();
            SetWorkplaces(startPoint);
        }

        private void SetWorkplaces(Transform startPoint)
        {
            for (int i = 0; i < startPoint.childCount; i++)
            {
                var cur = startPoint.GetChild(i).gameObject;
                if (cur.tag != "workPlace")
                    Debug.LogError("Not a workplace trying to be added to workplaces");
                else
                    workplaces.Add(cur);
            }
        }
        // TODO : add methods to show stats
    }

    /// <summary>
    /// Class which holds the housing units present in the city
    /// it contains methods for showing them
    /// </summary>
    public class HuUnits
    {
        public List<GameObject> huUnits;

        public HuUnits(Transform startPoint)
        {
            huUnits = new List<GameObject>();
            SethuUnits(startPoint);
        }

        private void SethuUnits(Transform startPoint)
        {
            for (int i = 0; i < startPoint.childCount; i++)
            {
                var cur = startPoint.GetChild(i).gameObject;
                if (cur.tag != "housingUnit")
                    Debug.LogError("Not a housing unit trying to be added to HuUnits");
                else
                    huUnits.Add(cur);
            }
        }
        // TODO : add methods to show stats
    }

    /// <summary>
    /// Class which holds the housing units present in the city
    /// it contains methods for showing them
    /// </summary>
    public class Marketplaces
    {
        public List<GameObject> marketplaces;

        public Marketplaces(Transform startPoint)
        {
            marketplaces = new List<GameObject>();
            SetMarketplaces(startPoint);
        }

        private void SetMarketplaces(Transform startPoint)
        {
            for (int i = 0; i < startPoint.childCount; i++)
            {
                var cur = startPoint.GetChild(i).gameObject;
                if (cur.tag != "supermarket")
                    Debug.LogError("Not a market trying to be added to marketplaces");
                else
                    marketplaces.Add(cur);
            }
        }
        // TODO : add methods to show stats
    }



    public Transform roadsTrans;
    public Transform workplacesTrans;
    public Transform housingUnitTrans;
    public Transform marketsTrans;

    private Boundaries boundaries;
    private Workplaces workplaces;
    private HuUnits huUnits;
    private Marketplaces marketplaces;

    public Boundaries GetBoundaries()
    {
        boundaries = new Boundaries
        {
            xMin = Mathf.Infinity,
            xMax = Mathf.NegativeInfinity,
            zMin = Mathf.Infinity,
            zMax = Mathf.NegativeInfinity
        };
        boundaries.SetBoundaries(roadsTrans);
        return boundaries;
    }
    public Workplaces GetWorkplaces()
    {
        workplaces = new Workplaces(workplacesTrans);
        return workplaces;
    }
    public HuUnits GetHuUnits()
    {
        huUnits = new HuUnits(housingUnitTrans);
        return huUnits;
    }
    public Marketplaces GetMarketplaces()
    {
        marketplaces = new Marketplaces(marketsTrans);
        return marketplaces;
    }

    void Start()
    {
        GetBoundaries().pointOnGridInside();
    }


    
}
