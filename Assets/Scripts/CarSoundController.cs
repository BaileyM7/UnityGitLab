using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    public AudioSource crashNoise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision other) {
        if (other.gameObject.CompareTag("Car")) {
            crashNoise.Play();
        }
    }
}
