using UnityEngine;
using System.Collections; // for Coroutines
using System.Collections.Generic;
using System;
using JetBrains.Annotations; // for List

public class SpawnCustomers : MonoBehaviour
{
    public GameObject[] customerPrefabs; // array of different prefabs
    public Transform spawnPoint;         // location where prefabs will spawn
    public Transform targetPoint;        // destination location
    public float moveSpeed = 2f;         // speed of movement
    public float minSpawnDelay = 1f;     // minimum time between spawns
    public float maxSpawnDelay = 5f;     // maximum time between spawns

    public static List<GameObject> activeCustomers = new List<GameObject>(); // Track active customers
    public static int maxCustomers = 1; // maximum number of customers allowed at a time

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
            float delay = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            // check if there are fewer than the maximum allowed customers
            if (activeCustomers.Count < maxCustomers)
            {
                SpawnAndMoveCustomer();
            }
            else
            {

            }
        }
    }

    private void SpawnAndMoveCustomer()
    {
        // picks a random prefab
        int n = UnityEngine.Random.Range(0, customerPrefabs.Length);
        try
        {
            GameObject selectedPrefab = customerPrefabs[n];

            Debug.Log(selectedPrefab);

            // define a 180-degree rotation around the Y-axis
            Quaternion rotation = Quaternion.Euler(0, 180, 0);

            // spawn the prefab at the spawn point with the specified rotation
            GameObject spawnedCustomer = Instantiate(selectedPrefab, spawnPoint.position, rotation);

            // add the spawned customer to the active list
            activeCustomers.Add(spawnedCustomer);

            // start sliding the customer to the target point
            StartCoroutine(SlideToTarget(spawnedCustomer, targetPoint.position));
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log(gameObject);
        }
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

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the pizza box
        if (other.CompareTag("Pizzabox"))
        {
            Debug.Log("Pizza box collided with customer!");

            // Remove the customer from the active list
            if (activeCustomers.Contains(gameObject))
            {
                activeCustomers.Remove(gameObject);
                Debug.Log("Customer removed from active list.");
            }
            // Destroy the customer GameObject
            Destroy(gameObject);

            Debug.Log("Customer destroyed after receiving the pizza.");
        }
    }

    public static void Complete(GameObject customer)
    {
        // Remove the customer from the active list
        if (activeCustomers.Contains(customer))
        {
            activeCustomers.Remove(customer);
            Debug.Log("Customer removed from active list.");
        }
    }



    public void RemoveAndDestroyCustomer(GameObject customer)
    {
        if (activeCustomers.Contains(customer))
        {
            activeCustomers.Remove(customer); // Remove from the active list
            Destroy(customer); // Destroy the GameObject
            Debug.Log("Customer destroyed after correct order.");
        }
    }

}



