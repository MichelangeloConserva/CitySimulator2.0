using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCross : MonoBehaviour {
    int got = 0;
    public List<Vector3> firstCrossPoints;
    public List<Vector3> secondCrossPoints;
    public List<Vector3> allCrossPoints;
    public GameObject ducetto;
    public GameObject crossRoadBody;
    public GameObject crossWalk;
    public float incavoIncrocio;

    Vector3[] allCrossPointsArray;
    MeshFilter meshFilterBody;
    MeshFilter meshFilterCrossWalk;

    int pointsIndex = 0;
    int[] trisCrosswalk = new int[32 * 3];
    int[] tris = new int[32 * 3];
    Vector2[] uvs = new Vector2[32];

    private void Start()
    {
        meshFilterBody = crossRoadBody.GetComponent<MeshFilter>();
        meshFilterCrossWalk = crossWalk.GetComponent<MeshFilter>();
    }


    public void CrossroadCreator(GameObject road)
    {
        got++;
        if (got == 1)
        {
            firstCrossPoints = road.GetComponent<RoadSpawn>().crossPoints;
        }
        else if (got == 2)
        {
            secondCrossPoints = road.GetComponent<RoadSpawn>().crossPoints;


            //prendiamo i punti di stacco e li mettiamo tutti in una lista
            for (int i = 0; i < 8; i++)
            {
                allCrossPoints.Add(firstCrossPoints[i]);
            }
            foreach (Vector3 v in secondCrossPoints)
            {
                allCrossPoints.Add(v);
            }

            //calcoliamo il rientro dalla strada
            allCrossPoints.Add(new Vector3(allCrossPoints[0].x + ((allCrossPoints[4].x - allCrossPoints[0].x) * incavoIncrocio),
                   allCrossPoints[0].y + ((allCrossPoints[4].y - allCrossPoints[0].y) * incavoIncrocio),
                   allCrossPoints[0].z + ((allCrossPoints[4].z - allCrossPoints[0].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[1].x + ((allCrossPoints[5].x - allCrossPoints[1].x) * incavoIncrocio),
                   allCrossPoints[1].y + ((allCrossPoints[5].y - allCrossPoints[1].y) * incavoIncrocio),
                   allCrossPoints[1].z + ((allCrossPoints[5].z - allCrossPoints[1].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[2].x + ((allCrossPoints[6].x - allCrossPoints[2].x) * incavoIncrocio),
           allCrossPoints[2].y + ((allCrossPoints[6].y - allCrossPoints[2].y) * incavoIncrocio),
           allCrossPoints[2].z + ((allCrossPoints[6].z - allCrossPoints[2].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[3].x + ((allCrossPoints[7].x - allCrossPoints[3].x) * incavoIncrocio),
           allCrossPoints[3].y + ((allCrossPoints[7].y - allCrossPoints[3].y) * incavoIncrocio),
           allCrossPoints[3].z + ((allCrossPoints[7].z - allCrossPoints[3].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[4].x + ((allCrossPoints[0].x - allCrossPoints[4].x) * incavoIncrocio),
                   allCrossPoints[4].y + ((allCrossPoints[0].y - allCrossPoints[4].y) * incavoIncrocio),
                   allCrossPoints[4].z + ((allCrossPoints[0].z - allCrossPoints[4].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[5].x + ((allCrossPoints[1].x - allCrossPoints[5].x) * incavoIncrocio),
                   allCrossPoints[5].y + ((allCrossPoints[1].y - allCrossPoints[5].y) * incavoIncrocio),
                   allCrossPoints[5].z + ((allCrossPoints[1].z - allCrossPoints[5].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[6].x + ((allCrossPoints[2].x - allCrossPoints[6].x) * incavoIncrocio),
           allCrossPoints[6].y + ((allCrossPoints[2].y - allCrossPoints[6].y) * incavoIncrocio),
           allCrossPoints[6].z + ((allCrossPoints[2].z - allCrossPoints[6].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[7].x + ((allCrossPoints[3].x - allCrossPoints[7].x) * incavoIncrocio),
           allCrossPoints[7].y + ((allCrossPoints[3].y - allCrossPoints[7].y) * incavoIncrocio),
           allCrossPoints[7].z + ((allCrossPoints[3].z - allCrossPoints[7].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[8].x + ((allCrossPoints[12].x - allCrossPoints[8].x) * incavoIncrocio),
                   allCrossPoints[8].y + ((allCrossPoints[12].y - allCrossPoints[8].y) * incavoIncrocio),
                   allCrossPoints[8].z + ((allCrossPoints[12].z - allCrossPoints[8].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[9].x + ((allCrossPoints[13].x - allCrossPoints[9].x) * incavoIncrocio),
                   allCrossPoints[9].y + ((allCrossPoints[13].y - allCrossPoints[9].y) * incavoIncrocio),
                   allCrossPoints[9].z + ((allCrossPoints[13].z - allCrossPoints[9].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[10].x + ((allCrossPoints[14].x - allCrossPoints[10].x) * incavoIncrocio),
           allCrossPoints[10].y + ((allCrossPoints[14].y - allCrossPoints[10].y) * incavoIncrocio),
           allCrossPoints[10].z + ((allCrossPoints[14].z - allCrossPoints[10].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[11].x + ((allCrossPoints[15].x - allCrossPoints[11].x) * incavoIncrocio),
           allCrossPoints[11].y + ((allCrossPoints[15].y - allCrossPoints[11].y) * incavoIncrocio),
           allCrossPoints[11].z + ((allCrossPoints[15].z - allCrossPoints[11].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[12].x + ((allCrossPoints[8].x - allCrossPoints[12].x) * incavoIncrocio),
                   allCrossPoints[12].y + ((allCrossPoints[8].y - allCrossPoints[12].y) * incavoIncrocio),
                   allCrossPoints[12].z + ((allCrossPoints[8].z - allCrossPoints[12].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[13].x + ((allCrossPoints[9].x - allCrossPoints[13].x) * incavoIncrocio),
                   allCrossPoints[13].y + ((allCrossPoints[9].y - allCrossPoints[13].y) * incavoIncrocio),
                   allCrossPoints[13].z + ((allCrossPoints[9].z - allCrossPoints[13].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[14].x + ((allCrossPoints[10].x - allCrossPoints[14].x) * incavoIncrocio),
           allCrossPoints[14].y + ((allCrossPoints[10].y - allCrossPoints[14].y) * incavoIncrocio),
           allCrossPoints[14].z + ((allCrossPoints[10].z - allCrossPoints[14].z) * incavoIncrocio)));

            allCrossPoints.Add(new Vector3(allCrossPoints[15].x + ((allCrossPoints[11].x - allCrossPoints[15].x) * incavoIncrocio),
           allCrossPoints[15].y + ((allCrossPoints[11].y - allCrossPoints[15].y) * incavoIncrocio),
           allCrossPoints[15].z + ((allCrossPoints[11].z - allCrossPoints[15].z) * incavoIncrocio)));

            var midPos = Vector3.zero;
            for (int i = 16; i < allCrossPoints.Count; i++)
                midPos += allCrossPoints[i];
            midPos /= allCrossPoints.Count - 16;
            midPos += Vector3.up / 5;
                
            foreach(GameObject s in road.GetComponent<RoadSpawn>().snapPointList)
                if (s.GetComponent<IsCollidingScript>().isColliding)
                {
                    s.transform.position = midPos;
                    s.GetComponent<IsCollidingScript>().otherSphere.transform.position = midPos;
                    break;
                }




            allCrossPointsArray = allCrossPoints.ToArray();
            meshFilterCrossWalk.mesh = BuildCrosswalk(allCrossPointsArray);
            meshFilterBody.mesh = BuildCrossroadBody(allCrossPointsArray);
            Instantiate(crossRoadBody);
            Instantiate(crossWalk);
            //clearing the variables
            allCrossPoints.Clear();
            allCrossPointsArray = allCrossPoints.ToArray();
            got = 0;
            firstCrossPoints.Clear();
            secondCrossPoints.Clear();
        }
    }

    Mesh BuildCrosswalk(Vector3[] allCrossPoints)
    {
        var incrementoPunti = 0;

        for (int i = 0; i < 4; i++)
        {
            if (i == 0 || i == 2)
            {
                //top face
                //top left
                trisCrosswalk[0 + incrementoPunti] = (pointsIndex);
                trisCrosswalk[1 + incrementoPunti] = (pointsIndex + 16);
                trisCrosswalk[2 + incrementoPunti] = (pointsIndex + 1);
                //top right
                trisCrosswalk[3 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[4 + incrementoPunti] = (pointsIndex + 16);
                trisCrosswalk[5 + incrementoPunti] = (pointsIndex + 17);
                //left face
                //left left
                trisCrosswalk[6 + incrementoPunti] = (pointsIndex + 2);
                trisCrosswalk[7 + incrementoPunti] = (pointsIndex + 18);
                trisCrosswalk[8 + incrementoPunti] = (pointsIndex + 16);
                //left right
                trisCrosswalk[9 + incrementoPunti] = (pointsIndex + 2);
                trisCrosswalk[10 + incrementoPunti] = (pointsIndex + 16);
                trisCrosswalk[11 + incrementoPunti] = (pointsIndex);
                //right face
                //right left
                trisCrosswalk[12 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[13 + incrementoPunti] = (pointsIndex + 19);
                trisCrosswalk[14 + incrementoPunti] = (pointsIndex + 3);
                //right right
                trisCrosswalk[15 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[16 + incrementoPunti] = (pointsIndex + 17);
                trisCrosswalk[17 + incrementoPunti] = (pointsIndex + 19);
            }
            else
            {
                //top face
                //top left
                trisCrosswalk[0 + incrementoPunti] = (pointsIndex);
                trisCrosswalk[1 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[2 + incrementoPunti] = (pointsIndex + 16);
                //top right
                trisCrosswalk[3 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[4 + incrementoPunti] = (pointsIndex + 17);
                trisCrosswalk[5 + incrementoPunti] = (pointsIndex + 16);
                //left face
                //left left
                trisCrosswalk[6 + incrementoPunti] = (pointsIndex + 2);
                trisCrosswalk[7 + incrementoPunti] = (pointsIndex + 16);
                trisCrosswalk[8 + incrementoPunti] = (pointsIndex + 18);
                //left right
                trisCrosswalk[9 + incrementoPunti] = (pointsIndex + 2);
                trisCrosswalk[10 + incrementoPunti] = (pointsIndex);
                trisCrosswalk[11 + incrementoPunti] = (pointsIndex + 16);
                //right face
                //right left
                trisCrosswalk[12 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[13 + incrementoPunti] = (pointsIndex + 3);
                trisCrosswalk[14 + incrementoPunti] = (pointsIndex + 19);
                //right right
                trisCrosswalk[15 + incrementoPunti] = (pointsIndex + 1);
                trisCrosswalk[16 + incrementoPunti] = (pointsIndex + 19);
                trisCrosswalk[17 + incrementoPunti] = (pointsIndex + 17);
            }

            incrementoPunti += 18;
            pointsIndex += 4;
        }
        for (int i=0; i<allCrossPoints.Length; i++)
            uvs[i] = new Vector2(Mathf.Abs(allCrossPoints[i].x), Mathf.Abs(allCrossPoints[i].z));

        //crosswalk orizzontale
        uvs[0] = new Vector2(0, 0.83f);
        uvs[0 + 16] = new Vector2(0, 1f);
        uvs[1] = new Vector2(0.73f, 0.83f);
        uvs[1 + 16] = new Vector2(0.73f, 1);

        uvs[4] = new Vector2(0, 0.83f);
        uvs[4 + 16] = new Vector2(0, 1);
        uvs[5] = new Vector2(0.73f, 0.83f);
        uvs[5 + 16] = new Vector2(0.73f, 1);

        //crosswalk verticale
        uvs[12] = new Vector2(0, 0);
        uvs[12 + 16] = new Vector2(0.16f, 0);
        uvs[13] = new Vector2(0, 0.71f);
        uvs[13 + 16] = new Vector2(0.16f, 0.71f);

        uvs[8] = new Vector2(0, 0);
        uvs[8 + 16] = new Vector2(0.16f, 0);
        uvs[9] = new Vector2(0, 0.71f);
        uvs[9 + 16] = new Vector2(0.16f, 0.71f);

        pointsIndex = 0;
        Mesh buildCrosswalk = new Mesh();
        buildCrosswalk.vertices = allCrossPoints;
        buildCrosswalk.triangles = trisCrosswalk;
        buildCrosswalk.uv = uvs;
        buildCrosswalk.RecalculateNormals();
        return buildCrosswalk;
    }

    Mesh BuildCrossroadBody(Vector3[] allCrossPoints)
    {
        //capiamo se abbiamo una disposizione di punti o un' altra
        if (Vector3.Distance(allCrossPoints[0 + 16], allCrossPoints[12 + 16]) < Vector3.Distance(allCrossPoints[0 + 16], allCrossPoints[9 + 16]))
        {
            //top faces
            //top left
            tris[0] = (pointsIndex + 0 + 16);
            tris[1] = (pointsIndex + 12 + 16);
            tris[2] = (pointsIndex + 13 + 16);

            tris[3] = (pointsIndex + 0 + 16);
            tris[4] = (pointsIndex + 13 + 16);
            tris[5] = (pointsIndex + 4 + 16);
            //top center
            tris[6] = (pointsIndex + 0 + 16);
            tris[7] = (pointsIndex + 4 + 16);
            tris[8] = (pointsIndex + 5 + 16);

            tris[9] = (pointsIndex + 0 + 16);
            tris[10] = (pointsIndex + 5 + 16);
            tris[11] = (pointsIndex + 1 + 16);
            //top right
            tris[12] = (pointsIndex + 5 + 16);
            tris[13] = (pointsIndex + 9 + 16);
            tris[14] = (pointsIndex + 8 + 16);

            tris[15] = (pointsIndex + 5 + 16);
            tris[16] = (pointsIndex + 8 + 16);
            tris[17] = (pointsIndex + 1 + 16);

            //lati
            tris[18] = (pointsIndex + (0 + 16));
            tris[19] = (pointsIndex + (2 + 16));
            tris[20] = (pointsIndex + (12 + 16));

            tris[21] = (pointsIndex + 2 + 16);
            tris[22] = (pointsIndex + 14 + 16);
            tris[23] = (pointsIndex + 12 + 16);


            tris[24] = (pointsIndex + 13 + 16);
            tris[25] = (pointsIndex + 15 + 16);
            tris[26] = (pointsIndex + 6 + 16);

            tris[27] = (pointsIndex + 4 + 16);
            tris[28] = (pointsIndex + 13 + 16);
            tris[29] = (pointsIndex + 6 + 16);


            tris[30] = (pointsIndex + 5 + 16);
            tris[31] = (pointsIndex + 7 + 16);
            tris[32] = (pointsIndex + 9 + 16);

            tris[33] = (pointsIndex + 7 + 16);
            tris[34] = (pointsIndex + 9 + 16);
            tris[35] = (pointsIndex + 11 + 16);


            tris[36] = (pointsIndex + 1 + 16);
            tris[37] = (pointsIndex + 8 + 16);
            tris[38] = (pointsIndex + 10 + 16);

            tris[39] = (pointsIndex + 1 + 16);
            tris[40] = (pointsIndex + 10 + 16);
            tris[41] = (pointsIndex + 3 + 16);                   
        }
        else
        {
            //top faces
            //top left
            tris[0] = (pointsIndex + 0 + 16);
            tris[1] = (pointsIndex + 8 + 16);
            tris[2] = (pointsIndex + 4 + 16);

            tris[3] = (pointsIndex + 0 + 16);
            tris[4] = (pointsIndex + 9 + 16);
            tris[5] = (pointsIndex + 8 + 16);
            //top center
            tris[6] = (pointsIndex + 0 + 16);
            tris[7] = (pointsIndex + 4 + 16);
            tris[8] = (pointsIndex + 5 + 16);

            tris[9] = (pointsIndex + 0 + 16);
            tris[10] = (pointsIndex + 5 + 16);
            tris[11] = (pointsIndex + 1 + 16);
            //top right
            tris[12] = (pointsIndex + 5 + 16);
            tris[13] = (pointsIndex + 12 + 16);
            tris[14] = (pointsIndex + 13 + 16);

            tris[15] = (pointsIndex + 5 + 16);
            tris[16] = (pointsIndex + 13 + 16);
            tris[17] = (pointsIndex + 1 + 16);

            //lati
            tris[18] = (pointsIndex + 0 + 16);
            tris[19] = (pointsIndex + 2 + 16);
            tris[20] = (pointsIndex + 9 + 16);

            tris[21] = (pointsIndex + 2 + 16);
            tris[22] = (pointsIndex + 11 + 16);
            tris[23] = (pointsIndex + 9 + 16);


            tris[24] = (pointsIndex + 8 + 16);
            tris[25] = (pointsIndex + 10 + 16);
            tris[26] = (pointsIndex + 6 + 16);

            tris[27] = (pointsIndex + 8 + 16);
            tris[28] = (pointsIndex + 6 + 16);
            tris[29] = (pointsIndex + 4 + 16);


            tris[30] = (pointsIndex + 5 + 16);
            tris[31] = (pointsIndex + 7 + 16);
            tris[32] = (pointsIndex + 12 + 16);

            tris[33] = (pointsIndex + 7 + 16);
            tris[34] = (pointsIndex + 14 + 16);
            tris[35] = (pointsIndex + 12 + 16);


            tris[36] = (pointsIndex + 1 + 16);
            tris[37] = (pointsIndex + 13 + 16);
            tris[38] = (pointsIndex + 15 + 16);

            tris[39] = (pointsIndex + 1 + 16);
            tris[40] = (pointsIndex + 15 + 16);
            tris[41] = (pointsIndex + 3 + 16);
        }

        for (int i = 0; i < allCrossPoints.Length; i++)
            uvs[i] = new Vector2(Mathf.Abs(allCrossPoints[i].x), Mathf.Abs(allCrossPoints[i].z));

        Mesh buildCrossRoadBody = new Mesh();
        buildCrossRoadBody.vertices = allCrossPoints;
        buildCrossRoadBody.triangles = tris;
        buildCrossRoadBody.uv = uvs;
        return buildCrossRoadBody;

    }
}
