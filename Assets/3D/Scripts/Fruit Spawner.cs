using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FruitSpawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float gravityChange = 1f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    public float spinAnglesMinMax = 0.2f;

    public bool isResting = false;
    public float spawnDuration = 5f;
    public float roundWait = 2f;

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

        gravityChange = (float)(Mathf.Floor(GameInit.DifficultyLevel / 10) * 0.5 + 1);
        Physics.gravity = new Vector3(0, -9.81f * gravityChange, 0);

        Debug.Log(GameInit.DifficultyLevel);

        int moduleOfLevel = GameInit.DifficultyLevel % 10;
        maxSpawnDelay = (float)(3.3 - (0.3 * moduleOfLevel));

        while (enabled)
        {
            float elapsedTime = 0f;
            float spawnDurationRange = Random.Range(spawnDuration - 1f, spawnDuration + 1f);
            float roundWaitRange = Random.Range(roundWait - 0.5f, roundWait + 0.5f);

            while (elapsedTime < spawnDurationRange)
            {
                GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

                if (Random.value < bombChance) {
                    prefab = bombPrefab;
                }

                Vector3 position = new Vector3
                {
                    x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                    z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                };

                Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                GameObject fruit = Instantiate(prefab, position, rotation);

                float launchAngle = Random.Range(minAngle, maxAngle);

                Vector3 launchDirection = Quaternion.AngleAxis(launchAngle, Vector3.forward) * Vector3.up;

                float force = Random.Range(minForce * Mathf.Sqrt(gravityChange), maxForce * Mathf.Sqrt(gravityChange));
                Rigidbody rb = fruit.GetComponent<Rigidbody>();
                
                rb.AddForce(launchDirection * force, ForceMode.Impulse);
                
                Vector3 randomTorque = new Vector3(
                    Random.Range(-spinAnglesMinMax, spinAnglesMinMax),  // Spin around X-axis
                    Random.Range(-spinAnglesMinMax, spinAnglesMinMax),  // Spin around Y-axis
                    Random.Range(-spinAnglesMinMax, spinAnglesMinMax)   // Spin around Z-axis
                );
                rb.AddTorque(randomTorque, ForceMode.Impulse);
                
                Destroy(fruit, maxLifetime);

                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);

                yield return new WaitForSeconds(delay);
                elapsedTime += delay;
            }

            yield return new WaitForSeconds(roundWaitRange);
        }
    }
}