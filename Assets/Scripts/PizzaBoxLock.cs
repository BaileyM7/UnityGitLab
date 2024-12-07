using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaBoxLock : MonoBehaviour
{
    public Transform lid; // Assign the lid Transform
    public Transform bottomBox; // Assign the bottom box Transform
    public float closeThreshold = 0.05f; // Distance threshold for lid closure
    public bool isLocked = false;

    private Rigidbody lidRb;
    private Rigidbody bottomRb;

    void Start()
    {
        lidRb = lid.GetComponent<Rigidbody>();
        bottomRb = bottomBox.GetComponent<Rigidbody>();
        if (bottomRb == null)
        {
            bottomRb = bottomBox.gameObject.AddComponent<Rigidbody>();
            //bottomRb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!isLocked && IsLidClosed())
        {
            LockBox();
        }
    }

    bool IsLidClosed()
    {
        return Vector3.Distance(lid.position, bottomBox.position) < closeThreshold;
    }

    void LockBox()
    {
        // Debug.Log("Box locked!");

        isLocked = true;

        // lock lid
        //lidRb.isKinematic = true;
        lid.SetParent(bottomBox);

        //bottomRb.isKinematic = false;
        XRGrabInteractable grabInteractable = bottomBox.GetComponent<XRGrabInteractable>();

    }


}
