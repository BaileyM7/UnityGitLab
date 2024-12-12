using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSoundController : MonoBehaviour
{
    public AudioSource splatNoise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision other) {
        if (other.gameObject.CompareTag("Dough") || other.gameObject.CompareTag("Sauce") || other.gameObject.CompareTag("Cheese") || other.gameObject.CompareTag("Pep") || other.gameObject.CompareTag("Topping")) {
            splatNoise.Play();
        }
    }
}
