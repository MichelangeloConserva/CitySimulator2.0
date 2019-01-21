using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    //[SerializeField]
    public bool isDragging = true;
    public bool isSnapping = false;
    private Vector3 mousePos;
    private float height = 0;
    public RoadSpawn roadSpawn;


    public bool IsDragging
    {
        get
        {
            return isDragging;
        }
        set
        {
            if (isDragging != value)
            {
                isDragging = value;
            }
        }
    }

    public bool IsSnapping
    {
        get
        {
            return isSnapping;
        }
        set
        {
            if (isSnapping != value)
            {
                isSnapping = value;
            }
        }
    }

    private void Start()
    {
        roadSpawn = GetComponentInParent<RoadSpawn>();
        IsDragging = true;
        RaycastHit hit;
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(raycast, out hit, 100.0f))
        {
            transform.position = hit.point;
        }
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            screenPoint.z));
        StartCoroutine(Spawned());
    }

    IEnumerator Spawned() 
    {
        while (IsDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            if (!IsSnapping)
            {
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                transform.position = Vector3.Lerp(transform.position, curPosition, Vector3.Distance(transform.position, curPosition));
            }
            if (Input.GetMouseButtonDown(0))
            {
                IsDragging = !IsDragging;
            }
            if (!IsDragging)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            screenPoint.z));
    }


    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = Vector3.Lerp(transform.position, curPosition, Vector3.Distance(transform.position, curPosition));
    }
}
