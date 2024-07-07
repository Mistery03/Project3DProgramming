using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject bunnyPrefab;
    public GameObject uraniumPrefab;
    public GameObject teleporter;
    public GameObject waterPrefab;
    public GameObject rockPrefab;

    public int width = 100;
    public int height = 100;
    public float scale = 10f;
    public float maxCubeHeight = 2f;
    public float minCubeHeight = 0.5f;

    public float rockChance = 0.01f;
    public float uraniumChance = 0.0001f;

    public float waterLevel = 0.5f; // The height level for water
    public int waterAreaSize = 20; // Size of the water area
    public int teleporterAreaSize = 10; // Size of the special cube area

    public int bunnyCount = 2;

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
            teleporterStartX = Random.Range(20, width - teleporterAreaSize - 20);
            teleporterStartZ = Random.Range(20, height - teleporterAreaSize - 20);
        }
        while (IsOverlapping(waterStartX, waterStartZ, waterAreaSize, teleporterStartX, teleporterStartZ, teleporterAreaSize) ||
               IsTooClose(waterStartX, waterStartZ, waterAreaSize, teleporterStartX, teleporterStartZ, 20));

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

                // Generate multi-layer Perlin noise value
                float noiseValue = GetBumpyNoise(x, z);

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

                        Vector3 playerSpawnPosition = portPos + new Vector3(5, 3, -7);
                        GameManager.Instance.SetPlayerSpawnPoint(playerSpawnPosition);
                    }

                    continue; 
                }

                // Spawn objects with chances
                //spawnObject(uraniumPrefab, uraniumChance, x, z, cubeHeight, -2, 6.5f, 0, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.5f, 0.5f, 0.5f)); // Uniform scale for uranium
                spawnObject(rockPrefab, rockChance, x, z, cubeHeight, 0f, 1f, 0, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(5f, 5f, 5f)); // Variable scale for rocks
                spawnItem(uraniumPrefab, uraniumChance, x, z, cubeHeight, -2, 6.5f, 0);
            }
        }

        for (int i = 0; i < bunnyCount; i++)
        {
            Vector3 bunnyPosition = new Vector3(Random.Range(0, width), 0, Random.Range(0, height));
            float bunnyHeight = Mathf.Lerp(minCubeHeight, maxCubeHeight, GetBumpyNoise((int)bunnyPosition.x, (int)bunnyPosition.z));
            bunnyPosition.y = bunnyHeight;

            GameObject bunny = Instantiate(bunnyPrefab, bunnyPosition, Quaternion.identity);
            bunny.transform.parent = this.transform;

            GameManager.Instance.RegisterBunny(bunny);
        }
    }

    float GetBumpyNoise(int x, int z)
    {
        // Combine multiple layers of Perlin noise with different scales and intensities
        float noiseValue = 0f;
        float frequency = 4f;
        float amplitude = 1f;
        float persistence = 0.5f; // How much the amplitude decreases with each layer
        int octaves = 4; // Number of layers

        for (int i = 0; i < octaves; i++)
        {
            noiseValue += Mathf.PerlinNoise(x / (scale / frequency), z / (scale / frequency)) * amplitude;
            frequency *= 2f; // Increase frequency
            amplitude *= persistence; // Decrease amplitude
        }

        return noiseValue / (2f - 1f / Mathf.Pow(2f, octaves - 1)); // Normalize the noise value
    }

    bool IsOverlapping(int startX1, int startZ1, int size1, int startX2, int startZ2, int size2)
    {
        return startX1 < startX2 + size2 &&
            startX1 + size1 > startX2 &&
            startZ1 < startZ2 + size2 &&
            startZ1 + size1 > startZ2;
    }

    bool IsTooClose(int startX1, int startZ1, int size1, int startX2, int startZ2, int minDistance)
    {
        return Mathf.Abs(startX1 - startX2) < size1 + minDistance &&
            Mathf.Abs(startZ1 - startZ2) < size1 + minDistance;
    }

    void SpawnWater(int x, int z)
    {
        Vector3 waterPosition = new Vector3(x + waterAreaSize / 2, waterLevel, z + waterAreaSize / 2);
        GameObject water = Instantiate(waterPrefab, waterPosition, Quaternion.identity);
        water.transform.localScale = waterScale;
        water.transform.parent = this.transform;
    }

    void spawnObject(GameObject prefab, float chance, int x, int z, float cubeHeight, float xOffset, float heightOffset, float zOffset, Vector3 minScale, Vector3 maxScale)
    {
        if (Random.value < chance)
        {
            Vector3 position = new Vector3(x + xOffset, cubeHeight + heightOffset, z + zOffset);
            GameObject objectToBeSpawned = Instantiate(prefab, position, Quaternion.identity);

            // Randomize scale
            Vector3 scale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
            );
            objectToBeSpawned.transform.localScale = scale;

            objectToBeSpawned.transform.parent = this.transform;
        }
    }

    void spawnItem(GameObject prefab, float chance, int x, int z, float cubeHeight, float xOffset, float heightOffset, float zOffset)
    {
        if (Random.value < chance)
        {
            Vector3 position = new Vector3(x + xOffset, cubeHeight + heightOffset, z + zOffset);
            GameObject objectToBeSpawned = Instantiate(prefab, position, Quaternion.identity);
            objectToBeSpawned.transform.parent = this.transform;
        }
    }
}
