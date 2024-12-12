using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderFulfillment : MonoBehaviour
{

    public AudioSource victoryNoise;
    public AudioSource wrongOrderNoise;
    public Order expectedOrder = null;

    // public OrderScreenController osc = GetComponent<OrderScreenController>();
    public OrderScreenController osc;
    TMP_Text textbox = null;

    public void generateCustomerOrder(TMP_Text textbox)
    {
        if (expectedOrder != null) return;
        this.textbox = textbox;
        int n = UnityEngine.Random.Range(0, Order.OrderStrs.Length);
        string selectedWord = Order.OrderStrs[n];
        expectedOrder = Order.basicOrders[n];
        textbox.text = selectedWord;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered by:");
        Debug.Log(other.gameObject);
        // we only care about pizza-type objects
        if (!other.CompareTag("Pizzabox")) { return; }
        // behaviour when an order is recieved.
        if (isCorrectOrder(other.gameObject))
        {
            Debug.Log("Correct Order");
            Destroy(gameObject);
            try
            {
                osc.OrderCompleted(OrderFromCrust(GetPizzaInBox(other.gameObject)));
            }
            catch (InvalidOrder) { Debug.LogError("ERROR: Correct order was submitted to correct place but the order was not reperesented on the order screen. Likely because the order was never input in the register.\n"); }
            victoryNoise.Play();
        }
        else
        {
            Debug.Log("Wrong Order");
            wrongOrderNoise.Play();
            if (textbox != null) { textbox.text = "That was not my order"; }
        }

        SpawnCustomers.Complete(gameObject);

        //normal destroy doesn't work on the pizza (I think because sockets are stupid and mean)
        List<GameObject> list = new();
        RecursiveDestroy(GetPizzaInBox(other.gameObject).transform, list);
        foreach (GameObject go in list)
        {
            Destroy(go);
        }
        // finally destroy the box
        Destroy(other.gameObject);
    }

    void RecursiveDestroy(Transform go, List<GameObject> list)
    {
        list.Add(go.gameObject);
        foreach (Transform t in go)
        {
            RecursiveDestroy(t, list);
        }
    }

    public Order OrderFromCrust(GameObject crust)
    {
        if (crust == null) return null;
        bool hasCheese = false, hasSauce = false, hasPep = false, hasSsg = false;
        foreach (Transform ingredient in crust.transform.Find("scaleTranslator"))
        {
            if (ingredient.CompareTag("Cheese")) { hasCheese = true; }
            else if (ingredient.CompareTag("Sauce")) { hasSauce = true; }
            else if (ingredient.CompareTag("Pep")) { hasPep = true; }
            else if (ingredient.CompareTag("Topping")) { hasSsg = true; }
        }
        return new()
        {
            cheese = hasCheese,
            pep = hasPep,
            ssg = hasSsg,
            sauce = hasSauce
        };
    }

    GameObject GetPizzaInBox(GameObject box)
    {
        PizzaBox boxScript = box.GetComponent<PizzaBox>();
        if (boxScript == null)
        {
            Debug.LogError("Object was tagged as a box but had no PizzaBox script.");
            return null;
        }
        return boxScript.pizzaCrust;
    }

    bool isCorrectOrder(GameObject box)
    {
        GameObject crust = GetPizzaInBox(box);
        if (crust == null)
        {
            Debug.LogWarning("null crust");
            return false;
        }
        // what is on the pizza?
        Order o = OrderFromCrust(crust);

        // does it match the expected?
        return o.compare(expectedOrder);
    }
}
