using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshDrawer : MonoBehaviour
{
    public GameObject player;
    public Material positiveMaterial;
    public Material negativeMaterial;

    Mesh mesh;
    MeshRenderer meshRenderer;
    public Vector3[] vertices = new Vector3[4];
    public Vector3 normal;
    public Vector3 startPos;

    private void Start()
    {
        // Create a new Mesh
        mesh = new Mesh();
        mesh.name = "Triangle";

        // Set the vertices of the triangle
        vertices[0] = new Vector3(0, 0, 0); // Vertex 1
        vertices[1] = new Vector3(1, 0, 0); // Vertex 2
        vertices[2] = new Vector3(0, 1, 0); // Vertex 3
        vertices[3] = new Vector3(1, 1, 0); // Vertex 4
        mesh.vertices = vertices;

        // Define the triangle face using indices
        int[] triangleIndices = new int[6] { 0, 1, 2, 2, 1, 3 };
        mesh.triangles = triangleIndices;

        normal = GetNormalVector(vertices);

        // Set the normal for each vertex
        Vector3[] normals = new Vector3[4] { normal, normal, normal, normal };
        mesh.normals = normals;

        // Assign the created mesh to the MeshFilter component
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        meshRenderer = GetComponent<MeshRenderer>();

        // Recalculate the bounds of the mesh
        mesh.RecalculateBounds();

        // Recalculate the normals
        mesh.RecalculateNormals();
    }

    public void Update()
    {
        startPos = (vertices[0] + vertices[1] + vertices[2]) / 3;

        normal = GetNormalVector(vertices);
        DrawRay(normal);

        Vector3 dirVect = (player.transform.position - this.transform.position).normalized;
        Debug.DrawRay(startPos, dirVect * 100, Color.blue);

        //
        float dotProduct = Vector3.Dot(normal, dirVect);
        Debug.Log(dotProduct);

        if (dotProduct > 0)
        {
            meshRenderer.material = positiveMaterial;
            Debug.Log("Triangle is facing the player");
        }
        else

        {
            meshRenderer.material = negativeMaterial;
            Debug.Log("Triangle is facing away from the player");
        }

    }

    public void  DrawRay(Vector3 direction)
    {
        Debug.DrawRay(startPos, direction * 1000, Color.red);
    }

    public Vector3 GetNormalVector(Vector3[] vertices)
    {
        // Calculate the normal of the triangle
        Vector3 edge1 = vertices[1] - vertices[0];
        Vector3 edge2 = vertices[2] - vertices[0];

        return Vector3.Cross(edge1, edge2).normalized;
    }
}
