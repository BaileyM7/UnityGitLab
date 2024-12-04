using UnityEngine;
using System.Collections; // For Coroutines
using System.Collections.Generic; // For List

public class SpawnCustomers : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array of different prefabs
    public Transform spawnPoint;         // Location where prefabs will spawn
    public Transform targetPoint;        // Destination location
    public float moveSpeed = 2f;         // Speed of movement
    public float minSpawnDelay = 1f;     // Minimum time between spawns
    public float maxSpawnDelay = 5f;     // Maximum time between spawns

    private List<GameObject> activeCustomers = new List<GameObject>(); // Track active customers
    private int maxCustomers = 2; // Maximum number of customers allowed at a time

    private void Start()
    {
        // Start the coroutine for random spawning
        StartCoroutine(SpawnCustomersRandomly());
    }

    private IEnumerator SpawnCustomersRandomly()
    {
        while (true)
        {
            // Wait for a random delay between spawns
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            // Check if there are fewer than the maximum allowed customers
            if (activeCustomers.Count < maxCustomers)
            {
                SpawnAndMoveCustomer();
            } else {

            }
        }
    }

    private void SpawnAndMoveCustomer()
    {
        // Picks a random prefab
        GameObject selectedPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

        // Define a 180-degree rotation around the Y-axis
        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        // Spawn the prefab at the spawn point with the specified rotation
        GameObject spawnedCustomer = Instantiate(selectedPrefab, spawnPoint.position, rotation);

        // Add the spawned customer to the active list
        activeCustomers.Add(spawnedCustomer);

        // Start sliding the customer to the target point
        StartCoroutine(SlideToTarget(spawnedCustomer, targetPoint.position));
    }

    private IEnumerator SlideToTarget(GameObject customer, Vector3 targetPosition)
    {
        while (Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            // Move the customer smoothly towards the target position
            customer.transform.position = Vector3.MoveTowards(
                customer.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null; // Waits for the next frame
        }

        

    }
}

