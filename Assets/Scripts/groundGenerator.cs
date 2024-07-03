using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject bunnyPrefab;
    public GameObject uraniumPrefab;
    public GameObject teleporter;
    public GameObject waterPrefab;

    public int width = 100;
    public int height = 100;
    public float scale = 10f;
    public float maxCubeHeight = 2f;
    public float minCubeHeight = 0.5f;

    public float uraniumChance = 0.0001f;

    public float waterLevel = 0.5f; // The height level for water
    public int waterAreaSize = 20; // Size of the water area
    public int teleporterAreaSize = 10; // Size of the special cube area

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

        int teleporterStartX;
        int teleporterStartZ;

        do
        {
            teleporterStartX = Random.Range(0, width - teleporterAreaSize);
            teleporterStartZ = Random.Range(0, height - teleporterAreaSize);
        }
        while (IsOverlapping(waterStartX, waterStartZ, waterAreaSize, teleporterStartX, teleporterStartZ, teleporterAreaSize));

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

                if (x >= teleporterStartX && x < teleporterStartX + teleporterAreaSize && z >= teleporterStartZ && z < teleporterStartZ + teleporterAreaSize)
                {
                    if (x == teleporterStartX + teleporterAreaSize / 2 && z == teleporterStartZ + teleporterAreaSize / 2)
                    {
                        Vector3 portPos = new Vector3(x, cubeHeight / 2, z);
                        GameObject portObject = Instantiate(teleporter, portPos, Quaternion.identity);
                        portObject.transform.localScale = new Vector3(5, 1, 5);
                        portObject.transform.parent = this.transform;

                        // Set player spawn point near the special cube
                        Vector3 playerSpawnPosition = portPos + new Vector3(5, 3, -7);
                        GameManager.Instance.SetPlayerSpawnPoint(playerSpawnPosition);
                    }

                    continue; // Skip object spawning for the special cube area
                }


                // Spawn objects with chances
                spawnObject(uraniumPrefab, uraniumChance, x, z, cubeHeight, -2, 6.5f, 0);
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
