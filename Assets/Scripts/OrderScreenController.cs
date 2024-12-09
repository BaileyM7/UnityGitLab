using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderScreenController : MonoBehaviour
{
    const int MAX_REND_ORD = 8;
    public GameObject cheese;
    public GameObject pep;
    public GameObject sausage;
    public static int orderCount = 0;
    private readonly Vector3[] spawnPos = {new Vector3(1.85000002f,3.15350008f,-88.3889999f),
                          new Vector3(0.827000022f,3.1559999f,-88.3639984f),
                          new Vector3(-0.189999998f,3.15100002f,-88.401001f),
                          new Vector3(-1.20899999f,3.15100002f,-88.401001f),
                          new Vector3(1.85000002f,2.22850013f,-88.3889999f),
                          new Vector3(0.827000022f,2.23099995f,-88.3639984f),
                          new Vector3(-0.189999998f,2.22600007f,-88.401001f),
                          new Vector3(-1.20899999f,2.22600007f,-88.401001f)};

    private static readonly List<Order> orders = new();
    private static readonly List<GameObject> rendered = new();

    public void OrderCompleted(Order order)
    {
        if(order == null) {throw new InvalidOrder("Order may not be null");}
        int ind = -1;
        foreach (Order o in orders)
        {
            Debug.Log(o);
        }
        for (int i = 0; i < orderCount; i++)
        {
            Debug.Log(i);
            Debug.Log(orders[i].toString());
            if (orders[i].compare(order))
            {
                ind = i;
                break;
            }
        }
        if (ind == -1) { throw new InvalidOrder("Tried to remove an order that has not been stored in the order system.\n"); }
        orders.RemoveAt(ind);
        orderCount--;

        foreach (GameObject go in rendered)
        {
            Destroy(go);
        }
        rendered.Clear();
        for (int i = 0; i < MAX_REND_ORD && i < orders.Count; i++)
        {
            GameObject obj = orders[i].ssg ? sausage : orders[i].pep ? pep : cheese;
            GameObject spawned = Instantiate(obj, spawnPos[i], Quaternion.identity);
            spawned.transform.Rotate(Vector3.right, 90f, Space.World);
            rendered.Add(spawned);
        }
    }

    public void OnButtonClicked(string pizzaType)
    {
        if (pizzaType == "Cheese")
        {
            Order order = new()
            {
                sauce = true,
                cheese = true,
                ssg = false,
                pep = false
            };

            orders.Add(order);
            if (orderCount < spawnPos.Length)
            {
                GameObject spawned = Instantiate(cheese, spawnPos[orderCount], Quaternion.identity);

                spawned.transform.Rotate(Vector3.right, 90f, Space.World);
                rendered.Add(spawned);
            }
            orderCount++;
        }
        else if (pizzaType == "Pep")
        {
            Order order = new()
            {
                sauce = true,
                cheese = true,
                pep = true,
                ssg = false
            };
            orders.Add(order);
            if (orderCount < spawnPos.Length)
            {
                GameObject spawned = Instantiate(pep, spawnPos[orderCount], Quaternion.identity);

                spawned.transform.Rotate(Vector3.right, 90f, Space.World);
                rendered.Add(spawned);
            }
            orderCount++;
        }
        else
        {
            Order order = new()
            {
                sauce = true,
                cheese = true,
                ssg = true,
                pep = false
            };
            orders.Add(order);
            if (orderCount < spawnPos.Length)
            {
                GameObject spawned = Instantiate(sausage, spawnPos[orderCount], Quaternion.identity);

                spawned.transform.Rotate(Vector3.right, 90f, Space.World);
                rendered.Add(spawned);
            }
            orderCount++;
        }
    }


}
