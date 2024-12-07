using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnClickClose : MonoBehaviour
{
    [SerializeField] private Animator pizzaBoxAnimator; // Animator for lid animation
    [SerializeField] private Transform lid; // Reference to the lid Transform
    private XRGrabInteractable grabInteractable; // XR Grab Interactable component
    private bool hasClosedLid = false; // Tracks if the lid has been closed
    public Transform pizzaBox; // The main pizza box
    [SerializeField] private Rigidbody lidRigidbody; // Rigidbody for the lid
    [SerializeField] private Rigidbody boxRigidbody; // Rigidbody for the pizza box

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        pizzaBoxAnimator = GetComponent<Animator>();
        boxRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!hasClosedLid)
        {
            // Trigger lid closing animation
            pizzaBoxAnimator.SetTrigger("Active");
            hasClosedLid = true;
            //lid.SetParent(pizzaBox);
            //boxRigidbody.isKinematic = true;
            //lidRigidbody.isKinematic = true;
        }
    }

}
