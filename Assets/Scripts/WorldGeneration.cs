using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject treePrefab;

    public GameObject bushesPrefab;

    public GameObject specialCubePrefab;

    public GameObject bunnyPrefab;
    public int width = 100;
    public int height = 100;
    public float scale = 10f;
    public float maxCubeHeight = 2f;
    public float minCubeHeight = 0.5f;
    public float treeChance = 0.01f;

    public float bushChance = 0.001f;
    public int bunnyCount = 5;

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // Generate random starting position for the 20x20 area
        int startX = Random.Range(0, width - 20); // Ensure it doesn't exceed terrain bounds
        int startZ = Random.Range(0, height - 20); // Ensure it doesn't exceed terrain bounds

        // Calculate the middle position of the 20x20 area
        int middleX = startX + 15;
        int middleZ = startZ + 15;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(x, 0, z);

                // Generate Perlin noise value
                float noiseValue = Mathf.PerlinNoise(x / scale, z / scale);

                float cubeHeight = Mathf.Lerp(minCubeHeight, maxCubeHeight, noiseValue);

                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);

                cube.transform.localScale = new Vector3(1, cubeHeight, 1);
                cube.transform.position = new Vector3(x, cubeHeight / 2, z);

                cube.transform.parent = this.transform;

                // Check if the current position is within the randomly chosen 20x20 area
                if (x >= startX && x < startX + 20 && z >= startZ && z < startZ + 20)
                {
                    // Calculate the position of the middle of the 20x20 area
                    Vector3 specialCubePosition = new Vector3(middleX, cubeHeight / 2, middleZ);

                    // Offset the position to place the special cube in the middle
                    specialCubePosition -= new Vector3(5, 0, 5);

                    GameObject specialCube = Instantiate(specialCubePrefab, specialCubePosition, Quaternion.identity);
                    specialCube.transform.localScale = new Vector3(10, 10, 10); // Adjust size to 10x10
                    specialCube.transform.parent = this.transform;
                    continue; // Skip tree spawning in this area
                }

                // Decide whether to spawn a tree on this cube
                if (Random.value < treeChance)
                {
                    float treeHeight = treePrefab.transform.localScale.y;
                    Vector3 treePosition = new Vector3(x - 4, cubeHeight + 6.5f, z - 16);

                    GameObject tree = Instantiate(treePrefab, treePosition, Quaternion.identity);

                    tree.transform.parent = this.transform;
                }

                if (Random.value < bushChance)
                {
                    float bushHeight = bushesPrefab.transform.localScale.y;
                    Vector3 bushPosition = new Vector3(x-1.1f, cubeHeight-2.2f, z);

                    GameObject bush = Instantiate(bushesPrefab, bushPosition, Quaternion.identity);

                    bush.transform.parent = this.transform;
                }
            }
        }

        for (int i = 0; i < bunnyCount; i++)
        {
            Vector3 bunnyPosition = new Vector3(Random.Range(0, width), 0, Random.Range(0, height));
            float bunnyHeight = Mathf.Lerp(minCubeHeight, maxCubeHeight, Mathf.PerlinNoise(bunnyPosition.x / scale, bunnyPosition.z / scale));
            bunnyPosition.y = bunnyHeight;

            GameObject bunny = Instantiate(bunnyPrefab, bunnyPosition, Quaternion.identity);
            bunny.transform.parent = this.transform;

            GameManager.Instance.RegisterBunny(bunny); // Notify the GameManager
        }
    }




}
