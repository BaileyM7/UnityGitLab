using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCustomers : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array of different prefabs
    public Transform spawnPoint;         // Location where prefabs will spawn
    public Transform targetPoint;        // Destination location
    public float moveSpeed = 2f;         // Speed of movement

    private void Update()
    {
        // Check for space key press
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnAndMoveCustomer();
        }
    }

    private void SpawnAndMoveCustomer()
    {
        // picks a random prefab
        GameObject selectedPrefab = customerPrefabs[Random.Range(0, 4)];

        // spwans the prefab at the spawn point
        GameObject spawnedCustomer = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);

        // Start sliding the customer to the target point
        StartCoroutine(SlideToTarget(spawnedCustomer, targetPoint.position));
    }

    private System.Collections.IEnumerator SlideToTarget(GameObject customer, Vector3 targetPosition)
    {
        while (Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            //move the customer smoothly towards the target position
            customer.transform.position = Vector3.MoveTowards(
                customer.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null; // waits for the next frame
        }

    }
}

