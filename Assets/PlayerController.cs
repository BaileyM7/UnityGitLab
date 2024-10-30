using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public int maxFallDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // detect player falling into the void, move to origin
        if(player.transform.position.y < maxFallDist){
            player.transform.position = new Vector3(0,0,0);
        }
    }
}
