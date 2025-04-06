using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FruitSpawnerTraining : MonoBehaviour
{
    private Collider spawnArea;
    public GameObject[] fruitPrefabs;

    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 5f;

    public float minScale = 0.5f;  
    public float maxScale = 1.5f;
    public float maxLifetime = 10f;
    public int maxFruitsSpawn = 5;
    public float minScalePop = 0.4f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);

        int moduleOfLevel = GameInit.DifficultyLevel % 10;
        maxSpawnDelay = (float)(3.3 - (0.3 * moduleOfLevel));

        while (enabled)
        {
            Fruits[] fruits = FindObjectsOfType<Fruits>();

            if (fruits.Length < maxFruitsSpawn)
            {
                GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

                Vector3 position = new Vector3
                {
                    x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                    z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                };

                Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                GameObject fruit = Instantiate(prefab, position, rotation);

                maxScale = GameInit.fruitSize;

                float randomSize = Random.Range(minScale, maxScale);
                fruit.transform.localScale *= randomSize;

                Destroy(fruit, maxLifetime);

                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);

                yield return new WaitForSeconds(delay);
            }
        }
    }
}