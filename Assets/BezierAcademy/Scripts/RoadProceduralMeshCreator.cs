using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathCreatorAndSettings))]
//[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshRenderer))]
public class RoadProceduralMeshCreator : MonoBehaviour {

    
    private float spacing = 0.05f;

    private PathProcedural path;

    public int textureRepeat;

    float roadWidth = 14;
    float tiling = -3;

    public bool isCreated;
    public bool autoUpdate;

    public Material roadMat;
    public Mesh roadMesh;

    public Vector3[] points;
    public List<int> pointsToDelete;




    public void Start()
    {
        Material copyRoadMat = new Material(roadMat);
        transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = copyRoadMat;
        //referenza al path
        path = GetComponent<PathCreatorAndSettings>().path;
        pointsToDelete = new List<int>();
    }

    public void UpdateRoad()
    {
        points = path.CalculateEvenlySpacedPoints(spacing);

        //Manipolazione e storage della mesh in un altro gameObject figlio della strada
        roadMesh = CreateRoadMesh(points, path.IsClosed, pointsToDelete);
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = roadMesh; //Dove sta la mesh
        textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * .05f);
        transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1,textureRepeat);
    }

    Mesh CreateRoadMesh(Vector3[] points, bool isClosed, List<int> pointsToDelete)
    {
        Vector3[] verts = new Vector3[points.Length * 4];
        Vector2[] uvs = new Vector2[verts.Length];
        int numTris = 12 * (points.Length - 1) + ((isClosed) ? 2 : 0);
        int[] tris = new int[numTris * 3];
        int vertIndex = 0;
        int triIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 forward = Vector3.zero; //inizializziamo il vettore forward come vettore vuoto. Ci serve a capire la direzione della strada

            if (i < points.Length - 1 || isClosed)           
                forward += points[(i + 1) % points.Length] - points[i];   // se ci troviamo nel mezzo della strada, forward viene aumentato della direzione del prossimo punto
            
            if (i > 0 || isClosed)           
                forward += points[i] - points[(i - 1 + points.Length) % points.Length];
            
            forward.Normalize();

            Vector3 left = new Vector3(-forward.z, points[i].y, forward.x); // vettore per la direzione a sinistra
            Vector3 up = new Vector3(0, 0.05f, 0);

            verts[vertIndex] = points[i] + up + left  * roadWidth * .5f;
            verts[vertIndex + 1] = points[i] + up - left * roadWidth * .5f;
            verts[vertIndex + 2] = points[i] - up + left * roadWidth * .5f;
            verts[vertIndex + 3] = points[i] -up - left * roadWidth * .5f;

            float completionPercent = i / (float)(points.Length - 1);
            float v = 1 - Mathf.Abs(2 * completionPercent - 1);

            uvs[vertIndex] = new Vector2(0, v);
            uvs[vertIndex + 1] = new Vector2(2f, v);
            uvs[vertIndex + 2] = new Vector2(0, v);
            uvs[vertIndex + 3] = new Vector2(2f, v);

            if ((i < points.Length - 1 || isClosed) && !pointsToDelete.Contains(i))
            {
                //Top Face//
                //Top Left
                tris[triIndex] = vertIndex;
                tris[triIndex + 1] = (vertIndex + 5) % verts.Length;
                tris[triIndex + 2] = (vertIndex + 1) ;
                //Top Right
                tris[triIndex + 3] = vertIndex;
                tris[triIndex + 4] = (vertIndex + 4) % verts.Length;
                tris[triIndex + 5] = (vertIndex + 5) % verts.Length;

                //Bottom Face//
                //Bottom Left
                tris[triIndex + 6] = (vertIndex + 3) % verts.Length;
                tris[triIndex + 7] = (vertIndex + 6) % verts.Length;
                tris[triIndex + 8] = (vertIndex + 2) % verts.Length;
                //Bottom Right
                tris[triIndex + 9] =  (vertIndex + 3) % verts.Length;
                tris[triIndex + 10] = (vertIndex + 7) % verts.Length;
                tris[triIndex + 11] = (vertIndex + 6) % verts.Length;

                //Front Face//
                //Front Left
                tris[triIndex + 12] = (vertIndex + 5) % verts.Length;
                tris[triIndex + 13] = (vertIndex + 6) % verts.Length;
                tris[triIndex + 14] = (vertIndex + 7) % verts.Length;
                //Front Right
                tris[triIndex + 15] = (vertIndex + 5) % verts.Length;
                tris[triIndex + 16] = (vertIndex + 4) % verts.Length;
                tris[triIndex + 17] = (vertIndex + 6) % verts.Length;

                //Back Face//
                //Back Left
                tris[triIndex + 18] = vertIndex;
                tris[triIndex + 19] = (vertIndex + 3) % verts.Length;
                tris[triIndex + 20] = (vertIndex + 2) % verts.Length;
                //Back Right
                tris[triIndex + 21] = vertIndex;
                tris[triIndex + 22] = (vertIndex + 1) ;
                tris[triIndex + 23] = (vertIndex + 3) % verts.Length;

                //Left Face//
                //Left Left
                tris[triIndex + 24] = vertIndex;
                tris[triIndex + 25] = (vertIndex + 2) % verts.Length;
                tris[triIndex + 26] = (vertIndex + 6) % verts.Length;
                //Left Right
                tris[triIndex + 27] = vertIndex;
                tris[triIndex + 28] = (vertIndex + 6) % verts.Length;
                tris[triIndex + 29] = (vertIndex + 4) % verts.Length;

                //Right Face//
                //Right Left
                tris[triIndex + 30] = (vertIndex + 3) % verts.Length;
                tris[triIndex + 31] = (vertIndex + 1) % verts.Length;
                tris[triIndex + 32] = (vertIndex + 5) ;
                //Right Right
                tris[triIndex + 33] = (vertIndex + 3) % verts.Length;
                tris[triIndex + 34] = (vertIndex + 5) % verts.Length;
                tris[triIndex + 35] = (vertIndex + 7) % verts.Length;  

            }

            vertIndex += 4;
            triIndex += 36;            
        }
        Mesh road = new Mesh();
        road.vertices = verts;
        road.triangles = tris;
        road.uv = uvs;
        isCreated = true;

        return road;
    }
}
