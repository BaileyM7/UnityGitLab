using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is called when another object enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}