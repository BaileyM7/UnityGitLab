using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaBox : MonoBehaviour
{
    private bool opened = false;
    public Animator anim;

    public GameObject pizzaCrust = null;

    private XRSocketInteractor socketInteractor;

    void Awake()
    {
        socketInteractor = GetComponentInChildren<XRSocketInteractor>();
        if (socketInteractor != null)
        {
            Debug.Log("SUBSCRIBING PIZZA BOX DONT FORGET TO LIKE AND HIT THE BELL");
            socketInteractor.selectEntered.AddListener(OnSocket);
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

    public void OnSocket(SelectEnterEventArgs args)
    {
        pizzaCrust = args.interactableObject.transform.gameObject;
        Debug.Log($"Pizza box was just entered by {pizzaCrust}");
    }
}