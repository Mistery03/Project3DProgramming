using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject treePrefab;
    public GameObject bushesPrefab;
    public GameObject lab;
    public GameObject bunnyPrefab;
    public GameObject woodPrefab;
    public GameObject applePrefab;
<<<<<<< Updated upstream
    public GameObject uraniumPrefab;
    
=======

    public GameObject hole;

>>>>>>> Stashed changes
    public GameObject waterPrefab;

    public int width = 100;
    public int height = 100;
    public float scale = 10f;
    public float maxCubeHeight = 2f;
    public float minCubeHeight = 0.5f;

    public float treeChance = 0.01f;
    public float bushChance = 0.001f;
    public float woodChance = 0.001f;
    public float appleChance = 0.001f;
<<<<<<< Updated upstream
    public float uraniumChance = 0.01f;
=======
>>>>>>> Stashed changes

    public float waterLevel = 0.5f; // The height level for water
    public int waterAreaSize = 20; // Size of the water area
    public int specialCubeAreaSize = 20; // Size of the special cube area

    public int bunnyCount = 0;

    public Vector3 waterScale = new Vector3(60, 1, 60);

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // Generate random starting positions for the 20x20 water and special cube areas
        int waterStartX = Random.Range(0, width - waterAreaSize);
        int waterStartZ = Random.Range(0, height - waterAreaSize);

        int specialCubeStartX;
        int specialCubeStartZ;

        do
        {
            specialCubeStartX = Random.Range(0, width - specialCubeAreaSize);
            specialCubeStartZ = Random.Range(0, height - specialCubeAreaSize);
        }
        while (IsOverlapping(waterStartX, waterStartZ, waterAreaSize, specialCubeStartX, specialCubeStartZ, specialCubeAreaSize));

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Check if the current position is within the 20x20 water area
                if (x >= waterStartX && x < waterStartX + waterAreaSize && z >= waterStartZ && z < waterStartZ + waterAreaSize)
                {
                    SpawnWater(x, z);
                    continue; // Skip the rest of the loop to avoid spawning cubes and objects
                }

                Vector3 position = new Vector3(x, 0, z);

                // Generate Perlin noise value
                float noiseValue = Mathf.PerlinNoise(x / scale, z / scale);

                float cubeHeight = Mathf.Lerp(minCubeHeight, maxCubeHeight, noiseValue);

                // Spawn the cube
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.localScale = new Vector3(1, cubeHeight, 1);
                cube.transform.position = new Vector3(x, cubeHeight / 2, z);
                cube.transform.parent = this.transform;

                // Check if the current position is within the special cube area
                if (x >= specialCubeStartX && x < specialCubeStartX + specialCubeAreaSize && z >= specialCubeStartZ && z < specialCubeStartZ + specialCubeAreaSize)
                {
                    if (x == specialCubeStartX + specialCubeAreaSize / 2 && z == specialCubeStartZ + specialCubeAreaSize / 2)
                    {
                        Vector3 labPos = new Vector3(x, cubeHeight / 2, z);
                        GameObject labObject = Instantiate(lab, labPos, Quaternion.identity);
                        labObject.transform.localScale = new Vector3(10, 13, 10);
                        labObject.transform.parent = this.transform;

                        // Set player spawn point near the special cube
                        Vector3 playerSpawnPosition = labPos + new Vector3(5, 3, -7);
                        GameManager.Instance.SetPlayerSpawnPoint(playerSpawnPosition);
                    }

                    continue; // Skip object spawning for the special cube area
                }

                // Spawn objects with chances
                spawnObject(treePrefab, treeChance, x, z, cubeHeight, -4f, 6.5f, -16f);
                spawnObject(bushesPrefab, bushChance, x, z, cubeHeight, -1.1f, -2.2f, 0);
                spawnObject(woodPrefab, woodChance, x, z, cubeHeight, -2f, 0f, 0f);
                spawnObject(applePrefab, appleChance, x, z, cubeHeight, -2, 6.5f, 0);
<<<<<<< Updated upstream
                spawnObject(uraniumPrefab, uraniumChance, x, z, cubeHeight, -2, 6.5f, 0);
=======
>>>>>>> Stashed changes
            }
        }

        for (int i = 0; i < bunnyCount; i++)
        {
            Vector3 bunnyPosition = new Vector3(Random.Range(0, width), 0, Random.Range(0, height));
            float bunnyHeight = Mathf.Lerp(minCubeHeight, maxCubeHeight, Mathf.PerlinNoise(bunnyPosition.x / scale, bunnyPosition.z / scale));
            bunnyPosition.y = bunnyHeight;

            GameObject bunny = Instantiate(bunnyPrefab, bunnyPosition, Quaternion.identity);
            bunny.transform.parent = this.transform;

            GameManager.Instance.RegisterBunny(bunny);
        }
    }

    bool IsOverlapping(int startX1, int startZ1, int size1, int startX2, int startZ2, int size2)
    {
        return startX1 < startX2 + size2 + 10  &&
               startX1 + size1 + 10 > startX2 &&
               startZ1 < startZ2 + size2 + 10 &&
               startZ1 + size1 + 10 > startZ2;
    }

    void SpawnWater(int x, int z)
    {
        Vector3 waterPosition = new Vector3(x + waterAreaSize / 2, waterLevel, z + waterAreaSize / 2);
        GameObject water = Instantiate(waterPrefab, waterPosition, Quaternion.identity);
        water.transform.localScale = waterScale;
        water.transform.parent = this.transform;
    }

    void spawnObject(GameObject prefab, float chance, int x, int z, float cubeHeight, float xOffset, float heightOffset, float zOffset)
    {
        if (Random.value < chance)
        {
            Vector3 position = new Vector3(x + xOffset, cubeHeight + heightOffset, z + zOffset);
            GameObject objectToBeSpawned = Instantiate(prefab, position, Quaternion.identity);
            objectToBeSpawned.transform.parent = this.transform;
        }
    }
}
