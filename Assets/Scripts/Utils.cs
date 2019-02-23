using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    public enum TrafficLightLights
    {
        red,
        yellow,
        green
    }

    public enum DayTime
    {
        earlyMorning,
        breakfast,
        morning,
        launch,
        afternoon,
        dinner,
        evening,
    }


    /// <summary>
    /// Draws a dubug arrow defined through a raycast
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    /// <param name="duration"></param>
    /// <param name="arrowHeadLength"></param>
    /// <param name="arrowHeadAngle"></param>
    public static void DrawDebugArrow(Vector3 pos, Vector3 direction, Color color, float duration = -1, float arrowHeadLength = 1.25f, float arrowHeadAngle = 20.0f)
    {
        if (duration > 0)
        {
            Debug.DrawRay(pos, direction, color, duration);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color, Mathf.Infinity);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color, Mathf.Infinity);
        }
        else
        {
            Debug.DrawRay(pos, direction, color);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
        }
    }

    /// <summary>
    /// Returns uniques integer index given an exclusive maximum
    /// </summary>
    /// <param name="uniques"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int[] UniqueRandom(int uniques, int max)
    {
        var numbers = new List<int>(max);
        for (int i = 0; i < max; i++)
            numbers.Add(i);

        var randomNumbers = new int[uniques];
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            var thisNumber = Random.Range(0, numbers.Count);

            randomNumbers[i] = numbers[thisNumber];
            numbers.RemoveAt(thisNumber);
        }

        return randomNumbers;
    }


    /// <summary>
    /// Finding the nearest node within radius
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static NodeStreet NearestNode(Vector3 pos, float radius = 14)
    {
        NodeStreet nearNode = null;

        float minDist = Mathf.Infinity;
        var colls = Physics.OverlapSphere(pos, radius, LayerMask.GetMask("network"));

        foreach (Collider c in colls)
        {
            if (Vector3.Distance(pos, c.gameObject.transform.position) < minDist)
            {
                minDist = Vector3.Distance(pos, c.gameObject.transform.position);
                nearNode = c.gameObject.GetComponent<NodeHandler>().node;
            }
        }

        return nearNode;
    }



    public static void SendVehicleFromTo(NodeStreet startNode, NodeStreet endNode, GameObject vehicle)
    {
        var path = AStar.PathFromTo(startNode, endNode, vehicle);
        
        if (path.Count > 0)
        {
            vehicle.transform.LookAt(path[0].nodePosition);
            vehicle.GetComponent<VehicleAIController>().nextWaypoint = path[0];
            vehicle.GetComponent<VehicleAIController>().waypoints = path;
            vehicle.GetComponent<VehicleAIController>().arrivalNode = endNode;
            return;
        }
        Debug.Log("Path not found");
    }


    public static T RandomFromArray<T>(ref T[] arr)
    {
        return arr[Random.Range(0, arr.Length - 1)];
    }

    public static Vector3 Down(Vector3 v)
    {
        return v - Vector3.up * v.y;
    }



    //public delegate T del<T>(ref T[] arr);
    //del<float> myDel = (ref float[] arr) => RandomFromArray(ref arr);

}
