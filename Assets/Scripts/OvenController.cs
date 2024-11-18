using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OvenController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Dough")){
            Debug.Log("Crust");
            other.GetComponent<DoughLogic>().Bake();
        }
        else if (other.CompareTag("Sauce") || other.CompareTag("Cheese") || other.CompareTag("Topping"))
        {
            Debug.Log("Not a crust");
        } else{
            Debug.Log("Bad object in oven");
        }
    }
    
    void OnTriggerExit(Collider other){

    }
}
