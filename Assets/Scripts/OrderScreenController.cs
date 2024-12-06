using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



// import OrderFulfillment;
public class OrderScreenController : MonoBehaviour
{
    const int MAX_REND_ORD = 8;
    public GameObject cheese;
    public GameObject pep;
    public GameObject sausage;
    public static int orderCount = 0;
    private Vector3[] spawnPos = {new Vector3(1.85000002f,3.15350008f,-88.3889999f),
                          new Vector3(0.827000022f,3.1559999f,-88.3639984f),
                          new Vector3(-0.189999998f,3.15100002f,-88.401001f),
                          new Vector3(-1.20899999f,3.15100002f,-88.401001f),
                          new Vector3(1.85000002f,2.22850013f,-88.3889999f),
                          new Vector3(0.827000022f,2.23099995f,-88.3639984f),
                          new Vector3(-0.189999998f,2.22600007f,-88.401001f),
                          new Vector3(-1.20899999f,2.22600007f,-88.401001f)};

    private List<Order> orders = new();
    private List<GameObject> rendered = new();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OrderCompleted(Order order)
    {
        if (!orders.Remove(order)) { throw new InvalidOrder("Tried to remove an order that has not been stored in the order system.\n"); }
        foreach(GameObject go in rendered){
            Destroy(go);
        }
        rendered.Clear();
        for (int i = 0; i < MAX_REND_ORD; i++)
        {
            GameObject obj = orders[i].ssg ? sausage : orders[i].pep ? pep : cheese;
            GameObject spawned = Instantiate(obj, spawnPos[orderCount], Quaternion.identity);
            spawned.transform.Rotate(Vector3.right, 90f, Space.World);
        }
        orderCount--;
    }

    public void OnButtonClicked(string pizzaType)
    {
        if (pizzaType == "Cheese")
        {
            Order order = new Order();
            order.sauce = true;
            order.cheese = true;

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
            Order order = new Order();
            order.sauce = true;
            order.cheese = true;
            order.pep = true;
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
            Order order = new Order();
            order.sauce = true;
            order.cheese = true;
            order.ssg = true;
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
