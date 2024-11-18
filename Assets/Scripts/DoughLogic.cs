using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoughLogic : MonoBehaviour
{

    public List<GameObject> socketed = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform childTransform in this.transform)
        {
            XRSocketInteractor x = childTransform.GetComponent<XRSocketInteractor>();
            x.selectEntered.AddListener(OnSocket);
            x.selectExited.AddListener(OnUnsocket);
        }
    }

    void OnSocket(SelectEnterEventArgs args)
    {
        MonoBehaviour obj = args.interactableObject as MonoBehaviour;
        Debug.Log("SOCKETING");
        if (obj != null) { this.socketed.Add(obj.gameObject); }
    }

    void OnUnsocket(SelectExitEventArgs args)
    {
        MonoBehaviour obj = args.interactableObject as MonoBehaviour;
        Debug.Log("UNSOCKETING");
        if (obj != null) { this.socketed.Remove(obj.gameObject); }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Bake(){
        Debug.Log("Baking");
        foreach (GameObject ing in this.socketed){
            ing.transform.SetParent(this.transform);
        }
    }
}
