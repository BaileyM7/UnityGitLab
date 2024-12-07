using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaBoxLock : MonoBehaviour
{
    public Transform lid; // Assign the lid Transform
    public Transform bottomBox; // Assign the bottom box Transform
    public float closeThreshold = 0.05f; // Distance threshold for lid closure
    public bool isLocked = false; // Tracks if the lid is locked

    private Rigidbody lidRb; // Rigidbody of the lid
    private Rigidbody bottomRb; // Rigidbody of the bottom box

    private XRGrabInteractable boxGrabInteractable; // XR Grab Interactable for the bottom box
    private XRGrabInteractable lidGrabInteractable; // XR Grab Interactable for the lid

    void Start()
    {
        lidRb = lid.GetComponent<Rigidbody>();
        bottomRb = bottomBox.GetComponent<Rigidbody>();

        if (bottomRb == null)
        {
            bottomRb = bottomBox.gameObject.AddComponent<Rigidbody>();
        }

        boxGrabInteractable = bottomBox.GetComponent<XRGrabInteractable>();
        if (boxGrabInteractable == null)
        {
            boxGrabInteractable = bottomBox.gameObject.AddComponent<XRGrabInteractable>();
        }

        lidGrabInteractable = lid.GetComponent<XRGrabInteractable>();
        if (lidGrabInteractable == null)
        {
            lidGrabInteractable = lid.gameObject.AddComponent<XRGrabInteractable>();
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
        isLocked = true;

        // Lock the lid in place after it closes
        lidRb.isKinematic = true;
        lid.SetParent(bottomBox);
        lid.localPosition = Vector3.zero;
        lid.localRotation = Quaternion.identity;

        lidGrabInteractable.enabled = true;
        boxGrabInteractable.enabled = true;

        bottomRb.isKinematic = true;
        //Debug.Log("Box locked and ready to be grabbed.");
    }

    public void UnlockBox()
    {
        isLocked = false;

        lidRb.isKinematic = false;
        lid.SetParent(null);
        lidGrabInteractable.enabled = true;

        boxGrabInteractable.enabled = true;

       //Debug.Log("Box unlocked and can be interacted with.");
    }
}
