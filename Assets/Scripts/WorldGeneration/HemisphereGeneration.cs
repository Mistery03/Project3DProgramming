using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HemisphereGeneration : MonoBehaviour
{
    public int longitudeSegments = 24;
    public int latitudeSegments = 12;
    public float radius = 0.3f;

    public Material hemisMaterial;

    void Start()
    {
        GenerateHemisphere();
    }

    void GenerateHemisphere()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Calculate number of vertices and triangles
        int vertCount = (longitudeSegments + 1) * latitudeSegments + 1;
        int triCount = longitudeSegments * (latitudeSegments - 1) * 6 + longitudeSegments * 3;

        Vector3[] vertices = new Vector3[vertCount];
        int[] triangles = new int[triCount];
        Vector2[] uv = new Vector2[vertCount];

        int vertIndex = 0;
        int triIndex = 0;

        // Generate vertices
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            float a1 = Mathf.PI / 2 * lat / (latitudeSegments - 1);
            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                float a2 = 2 * Mathf.PI * lon / longitudeSegments;

                float x = Mathf.Cos(a2) * Mathf.Cos(a1);
                float y = Mathf.Sin(a1);
                float z = Mathf.Sin(a2) * Mathf.Cos(a1);

                vertices[vertIndex] = new Vector3(x, y, z) * radius;
                uv[vertIndex] = new Vector2((float)lon / longitudeSegments, (float)lat / (latitudeSegments - 1));
                vertIndex++;
            }
        }

        // Top vertex
        vertices[vertIndex] = Vector3.up * radius;
        uv[vertIndex] = new Vector2(0.5f, 1f);

        // Generate triangles
        for (int lat = 0; lat < latitudeSegments - 1; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int current = lat * (longitudeSegments + 1) + lon;
                int next = current + longitudeSegments + 1;

                triangles[triIndex++] = current;
                triangles[triIndex++] = next;
                triangles[triIndex++] = current + 1;

                triangles[triIndex++] = current + 1;
                triangles[triIndex++] = next;
                triangles[triIndex++] = next + 1;
            }
        }

        // Top triangles
        for (int lon = 0; lon < longitudeSegments; lon++)
        {
            triangles[triIndex++] = vertIndex;
            triangles[triIndex++] = (latitudeSegments - 1) * (longitudeSegments + 1) + lon;
            triangles[triIndex++] = (latitudeSegments - 1) * (longitudeSegments + 1) + lon + 1;
        }

        // Assign vertices, triangles, and UVs to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();

        if (hemisMaterial != null)
        {
            meshRenderer.material = hemisMaterial;
        }
        else
        {
            Debug.LogWarning("No material assigned to hemisMaterial.");
        }
    }
}
