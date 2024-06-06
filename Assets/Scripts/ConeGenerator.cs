using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ConeGenerator : MonoBehaviour
{
    public float radius = 0.5f;
    public float height = 1.0f;
    public int segments = 20;
    public Material coneMaterial;

    void Start()
    {
        GenerateCone();
    }

    void GenerateCone()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.name = "Cone";

        // Generate vertices
        Vector3[] vertices = new Vector3[segments + 2];
        vertices[0] = Vector3.zero; // Apex of the cone
        for (int i = 0; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            vertices[i + 1] = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        }
        vertices[segments + 1] = new Vector3(0, height, 0); // Center of the base

        // Generate triangles
        int[] triangles = new int[segments * 6];
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 6] = 0;
            triangles[i * 6 + 1] = i + 1;
            triangles[i * 6 + 2] = (i + 1) % segments + 1;

            triangles[i * 6 + 3] = segments + 1;
            triangles[i * 6 + 4] = (i + 1) % segments + 1;
            triangles[i * 6 + 5] = i + 1;
        }

        // Assign vertices and triangles to mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        if (coneMaterial != null)
        {
            meshRenderer.material = coneMaterial;
        }
        else
        {
            Debug.LogWarning("No material assigned to coneMaterial.");
        }
    }
}
