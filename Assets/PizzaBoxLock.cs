using UnityEngine;

public class PizzaBoxLock : MonoBehaviour
{
    public Transform lid; // Assign the lid Transform
    public Transform bottomBox; // Assign the bottom box Transform
    public int closeThreshold = 0; // Threshold for lid closure (wrong)
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
