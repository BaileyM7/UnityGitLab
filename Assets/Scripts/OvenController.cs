using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Zinnia.Data.Type;

public class OvenController : MonoBehaviour
{

    public class BakeInfo
    {
        public Color initialColor;
        public float timeInOven;
        public bool inOven;

        public BakeInfo(Color initialColor, float timeInOven = 0.0f, bool inOven = true)
        {
            this.initialColor = initialColor;
            this.timeInOven = timeInOven;
            this.inOven = inOven;
        }
    }


    private Dictionary<GameObject, BakeInfo> tracked = new Dictionary<GameObject, BakeInfo>();
    private float bakeTime = 15.0f;
    private Color crustTarget = new Color(240 / 255.0f, 206 / 255.0f, 105 / 255.0f);
    private Color cheeseTarget = new Color(240 / 255.0f, 226 / 255.0f, 72 / 255.0f);
    private Color pepTarget = new Color(173 / 255.0f, 54 / 255.0f, 35 / 255.0f);
    private int numInOven = 0;


    // opening animation
    private bool opened = false;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Change the color of each object incrementally
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject rend in tracked.Keys)
        {
            if (tracked[rend].inOven)
            {
                Color bakeColor = WhichTargetColor(rend);
                rend.GetComponent<Renderer>().material.color = Color.Lerp(tracked[rend].initialColor, bakeColor, tracked[rend].timeInOven / bakeTime);
                tracked[rend].timeInOven += Time.deltaTime;
                if (tracked[rend].timeInOven > bakeTime) { toRemove.Add(rend); } // stop tracking if no longer needed to update
            }
        }
        foreach (GameObject g in toRemove)
        {
            tracked.Remove(g);
        }
    }

    private Color WhichTargetColor(GameObject g)
    {
        return g.CompareTag("Dough") ? crustTarget
        : g.CompareTag("Cheese") ? cheeseTarget
        : g.CompareTag("Pep") ? pepTarget
        : g.GetComponent<Renderer>().material.color;
    }

    private void StartTracking(GameObject g)
    {
        Renderer rend = g.GetComponent<Renderer>();
        if (rend != null)
        {
            if (tracked.ContainsKey(g))
            {
                tracked[g].inOven = true;
                numInOven++;
            }
            else
            {
                tracked.Add(g, new BakeInfo(rend.material.color));
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dough"))
        {
            StartTracking(other.gameObject);
            other.GetComponent<DoughLogic>().Bake();
        }
        else if (other.CompareTag("Cheese"))
        {
            // single piece bake-ables (non-crust)
            StartTracking(other.gameObject);
            // StartCoroutine(GrabCleaner(other));
        }
        else if (other.CompareTag("Pep") || other.CompareTag("Topping"))
        {
            // multiple tiny things with individual materials
            foreach (Transform c in other.transform)
            {
                StartTracking(c.gameObject);
            }
            // StartCoroutine(GrabCleaner(other));
        }
        else
        {
            // Debug.Log("Bad object in oven");
        }
    }

    /*makes the grabbing of this object a cleaner action.*/
    IEnumerator GrabCleaner(Collider other)
    {
        // waiting so there's time to actually put the pizza in the oven before it starts gettin funky
        yield return new WaitForSeconds(4);
        Destroy(other.GetComponent<XRGrabInteractable>());
        Destroy(other.GetComponent<BoxCollider>());
        Destroy(other.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider other)
    {
        Renderer rend = other.GetComponent<Renderer>();
        if (rend != null && tracked.ContainsKey(rend.gameObject))
        {
            tracked[other.gameObject].inOven = false;
            numInOven--;
        }
    }


    // 1 - Close
    // 2 - Open
    public void onToggle()
    {
        opened = !opened;
        if (opened)
        {
            anim.Play("PizzaBoxOpen");
        }
        else
        {
            anim.Play("PizzaBoxClose");
        }
    }
}
