using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArrow : MonoBehaviour
{
    public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float duration, float arrowHeadLength = 1.25f, float arrowHeadAngle = 20.0f)
    {
        Debug.DrawRay(pos, direction, color, duration);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength, color, Mathf.Infinity);
        Debug.DrawRay(pos + direction, left * arrowHeadLength, color, Mathf.Infinity);
    }
}