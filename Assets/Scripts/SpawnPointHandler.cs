using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointHandler : MonoBehaviour
{
    public NodeStreet node;

    public int conn;

    void Start()
    {
        InitializeNode();
        StartCoroutine(Connect(transform.position + transform.forward * 7));
    }


    private IEnumerator Connect(Vector3 PosToCheck)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        var colls = Physics.OverlapSphere(PosToCheck, 5f, LayerMask.GetMask("network"));
        if (colls.Length == 0)
        {
            StartCoroutine(Connect(transform.position + transform.forward * 7 + transform.right * 5));
            yield return null;
        } else
        {
            NodeStreet nearestNode = colls[0].gameObject.GetComponent<NodeHandler>().node;
            foreach (Collider c in colls)
            {
                var nextNode = c.gameObject.GetComponent<NodeHandler>().node;

                if (Vector3.Distance(transform.position, c.gameObject.transform.position) <
                    Vector3.Distance(transform.position, nearestNode.nodePosition))
                    nearestNode = nextNode;
            }

            // Linking it to me
            var linkingStret = new ArcStreet(nearestNode, node);
            nearestNode.AddStreet(linkingStret);

            // Linking me to it
            var curStreet = new ArcStreet(node, nearestNode);
            node.AddStreet(curStreet);
        }
    }


    public void InitializeNode()
    {
        if (node == null)
            node = new NodeStreet(transform.position - (Vector3.up * transform.position.y));
        else
        {
            node.nodePosition = transform.position - (Vector3.up * transform.position.y);
            node.availableStreets.Clear();
        }
    }


    void Update()
    {
        conn = node.availableStreets.Count;

        if (Settings.visualizeRoadNetwork)
            for (int i = 0; i < conn; i++)
                DrawArrow.ForDebug(node.availableStreets[i].startNode.nodePosition + Vector3.up,
                        (node.availableStreets[i].arrivalNode.nodePosition + Vector3.up * (i + 1)) - (node.availableStreets[i].startNode.nodePosition + Vector3.up),
                        Color.white);
    }

    public void UpdateNodePos(Vector3 pos)
    {
        node.nodePosition = pos - (Vector3.up * pos.y);
    }
}
