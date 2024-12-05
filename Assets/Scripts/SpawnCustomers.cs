using UnityEngine;
using System.Collections; // for Coroutines
using System.Collections.Generic; // for List

public class SpawnCustomers : MonoBehaviour
{
    public GameObject[] customerPrefabs; // array of different prefabs
    public Transform spawnPoint;         // location where prefabs will spawn
    public Transform targetPoint;        // destination location
    public float moveSpeed = 2f;         // speed of movement
    public float minSpawnDelay = 1f;     // minimum time between spawns
    public float maxSpawnDelay = 5f;     // maximum time between spawns

    private List<GameObject> activeCustomers = new List<GameObject>(); // Track active customers
    private int maxCustomers = 2; // maximum number of customers allowed at a time

    private void Start()
    {
        // start the coroutine for random spawning
        StartCoroutine(SpawnCustomersRandomly());
    }

    private IEnumerator SpawnCustomersRandomly()
    {
        while (true)
        {
            // wait for a random delay between spawns
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            // check if there are fewer than the maximum allowed customers
            if (activeCustomers.Count < maxCustomers)
            {
                SpawnAndMoveCustomer();
            } else {

            }
        }
    }

    private void SpawnAndMoveCustomer()
    {
        // picks a random prefab
        GameObject selectedPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];
        Debug.Log(selectedPrefab.name);

        // define a 180-degree rotation around the Y-axis
        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        // spawn the prefab at the spawn point with the specified rotation
        GameObject spawnedCustomer = Instantiate(selectedPrefab, spawnPoint.position, rotation);

        // add the spawned customer to the active list
        activeCustomers.Add(spawnedCustomer);

        // start sliding the customer to the target point
        StartCoroutine(SlideToTarget(spawnedCustomer, targetPoint.position));
    }

    private IEnumerator SlideToTarget(GameObject customer, Vector3 targetPosition)
    {
        while (Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            // move the customer smoothly towards the target position
            customer.transform.position = Vector3.MoveTowards(
                customer.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null; // waits for the next frame
        }

        // remove the customer from the active list after they reach the target
        //activeCustomers.Remove(customer);

        // destroy the customer GameObject after reaching the target
        // Destroy(customer);

        

    }
}

