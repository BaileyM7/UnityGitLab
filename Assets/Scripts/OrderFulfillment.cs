using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFulfillment : MonoBehaviour
{

    public AudioSource victoryNoise;
    public AudioSource wrongOrderNoise;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // we only care about pizza-type objects
        if (!other.CompareTag("Dough")) { return; }
        // behaviour when an order is recieved.
        if (isCorrectOrder(other.gameObject))
        {
            Debug.Log("Correct Order");
            victoryNoise.Play();
        }
        else
        {
            Debug.Log("Wrong Order");
            wrongOrderNoise.Play();
        }

        List<GameObject> list = new();
        RecursiveDestroy(other.transform, list);
        foreach(GameObject go in list){
            Destroy(gameObject);
        }
    }

    void RecursiveDestroy(Transform go, List<GameObject> list){
        list.Add(go.gameObject);
        foreach(Transform t in go){
            RecursiveDestroy(t, list);
        }
    }

    void Consume(Transform t){
        foreach (Transform ingredient in t.Find("scaleTranslator")){
            Debug.Log("DESTROY:");
            Debug.Log(ingredient.gameObject);
            Destroy(ingredient.gameObject);
        }
        Destroy(t.gameObject);
    }

    bool isCorrectOrder(GameObject crust)
    {
        // what is on the pizza?
        bool hasCheese=false,hasSauce=false,hasPep=false,hasSsg = false;
        foreach (Transform ingredient in crust.transform.Find("scaleTranslator")){
            if (ingredient.CompareTag("Cheese")) {hasCheese = true;}
            else if (ingredient.CompareTag("Sauce")) {hasSauce = true;}
            else if (ingredient.CompareTag("Pep")) {hasPep = true;}
            else if (ingredient.CompareTag("Topping")) {hasSsg = true;}
        }

        // does it match the expected?
        //TODO using temp order for now
        Order temp = new Order();
        temp.cheese = true; temp.sauce = true; temp.pep = true;
        return temp.cheese == hasCheese && temp.sauce == hasSauce && temp.pep == hasPep && temp.ssg == hasSsg;
    }
}
