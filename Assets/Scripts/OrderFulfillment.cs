using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFulfillment : MonoBehaviour
{

    public AudioSource victoryNoise;
    public AudioSource wrongOrderNoise;

    public OrderScreenController osc;

    void OnTriggerEnter(Collider other)
    {
        // we only care about pizza-type objects
        if (!other.CompareTag("Dough")) { return; }
        // behaviour when an order is recieved.
        if (isCorrectOrder(other.gameObject))
        {
            Debug.Log("Correct Order");
            osc.OrderCompleted(OrderFromCrust(other.gameObject));
            victoryNoise.Play();
        }
        else
        {
            Debug.Log("Wrong Order");
            wrongOrderNoise.Play();
        }

        //normal destroy doesn't work (I think because sockets are stupid and mean)
        List<GameObject> list = new();
        RecursiveDestroy(other.transform, list);
        foreach(GameObject go in list){
            Destroy(go);
        }
    }

    void RecursiveDestroy(Transform go, List<GameObject> list){
        list.Add(go.gameObject);
        foreach(Transform t in go){
            RecursiveDestroy(t, list);
        }
    }

    public Order OrderFromCrust(GameObject crust){
        bool hasCheese=false,hasSauce=false,hasPep=false,hasSsg = false;
        foreach (Transform ingredient in crust.transform.Find("scaleTranslator")){
            if (ingredient.CompareTag("Cheese")) {hasCheese = true;}
            else if (ingredient.CompareTag("Sauce")) {hasSauce = true;}
            else if (ingredient.CompareTag("Pep")) {hasPep = true;}
            else if (ingredient.CompareTag("Topping")) {hasSsg = true;}
        }
        return new(){
            cheese = hasCheese,
            pep = hasPep,
            ssg = hasSsg,
            sauce = hasSauce
        };
    }

    bool isCorrectOrder(GameObject crust)
    {
        // what is on the pizza?
        Order o = OrderFromCrust(crust);

        // does it match the expected?
        //TODO using temp order for now
        Order temp = new()
        {
            cheese = true,
            sauce = true,
            pep = true,
            ssg=false
        };

        return o.compare(temp);
    }
}
