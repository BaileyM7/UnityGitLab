using UnityEngine;

public class CustomerCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the pizza box
        if (other.CompareTag("Pizzabox"))
        {
            // Destroy the customer
            Debug.Log("Pizza delivered! Customer leaving.");
            Destroy(gameObject);
        }
    }
}
