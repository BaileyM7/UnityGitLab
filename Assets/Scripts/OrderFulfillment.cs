using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order{
    public bool cheese,sauce,pep,ssg;
}

public class OrderFulfillment : MonoBehaviour
{

    AudioSource victoryNoise;
    AudioSource wrongOrderNoise;
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
            victoryNoise.Play();
        }
        else
        {
            wrongOrderNoise.Play();
        }
        Destroy(other);
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
        return temp.cheese == hasCheese && temp.sauce == hasSauce && temp.pep == hasPep && temp.ssg == hasSsg;
    }
}
