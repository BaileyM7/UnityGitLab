using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class DoughLogic : MonoBehaviour
{

    public List<GameObject> socketed = new List<GameObject>();

    private GameObject tempParent;

    // Start is called before the first frame update
    void Start()
    {
        tempParent = new GameObject("scaleTranslator");
        foreach (Transform childTransform in this.transform)
        {
            XRSocketInteractor x = childTransform.GetComponent<XRSocketInteractor>();
            if (x != null)
            {
                x.selectEntered.AddListener(OnSocket);
                x.selectExited.AddListener(OnUnsocket);
            }
        }
    }

    void OnSocket(SelectEnterEventArgs args)
    {
        MonoBehaviour obj = args.interactableObject as MonoBehaviour;
        Debug.Log($"SOCKETING {obj.name}");
        if (obj != null)
        {
            this.socketed.Add(obj.gameObject);
        }
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


    public void Bake()
    {
        foreach (GameObject ing in this.socketed)
        {
            // Vector3 originalScale = ing.transform.lossyScale;
            // Debug.Log("OG SCALE");
            // Debug.Log(originalScale);


            // GameObject temp = new GameObject("temp");
            tempParent.transform.position = ing.transform.position;
            tempParent.transform.rotation = ing.transform.rotation;
            ing.transform.SetParent(tempParent.transform);
            tempParent.transform.SetParent(this.transform);

            // ing.transform.localScale = new Vector3(
            //     originalScale.x / this.transform.lossyScale.x,
            //     originalScale.y / this.transform.lossyScale.y,
            //     originalScale.z / this.transform.lossyScale.z
            // );

            // ing.transform.SetParent(this.transform);
            // // Destroy(tempParent);

            // Debug.Log("NEW SCALE");
            // Debug.Log(ing.transform.localScale);
        }
    }
}
