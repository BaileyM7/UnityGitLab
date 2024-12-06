using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class onClickClose : MonoBehaviour
{
 [SerializeField] private Animator pizzaBoxAnimator;
    private XRGrabInteractable grabInteractable; // XR Grab Interactable component
    private bool hasClosedLid = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        pizzaBoxAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        // Unsubscribe from grab events
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!hasClosedLid)
        {
            pizzaBoxAnimator.SetTrigger("Active");
            hasClosedLid = true;
        }
    }
    }
