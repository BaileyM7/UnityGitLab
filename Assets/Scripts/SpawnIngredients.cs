using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredients : MonoBehaviour
{
    public Vector3 spawnLocation = new Vector3 (-3.272f,2.1f,-87.418f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn ingredient when box is clicked
    public void OnBoxClicked(string obj)
    {
        GameObject gameObject = Resources.Load<GameObject>("Pizza Stuff/" + obj);
        Instantiate(gameObject, spawnLocation, Quaternion.identity);
    }
}
