using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaBoxLock : MonoBehaviour
{
    public Transform lid; // Assign the lid Transform
    public Transform bottomBox; // Assign the bottom box Transform
    public float closeThreshold = .01f; // Threshold for lid closure (wrong)
    public bool isLocked = false;

    private Rigidbody lidRb;

    void Start()
    {
        lidRb = lid.GetComponent<Rigidbody>();
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
        lidRb.isKinematic = true;
        lid.SetParent(bottomBox);
    }

    public void UnlockBox()
    {
        isLocked = false;
        lidRb.isKinematic = false;
        lid.SetParent(null);
    }
}
