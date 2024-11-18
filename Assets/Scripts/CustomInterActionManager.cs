using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInterActionManager : XRInteractionManager
{
    public void ForceSelect(XRBaseInteractor interactor, IXRSelectInteractable interactable)
    {
        SelectEnter(interactor, interactable);

        if (interactor is XRRayInteractor rayInteractor)
        {
            interactable.transform.position = rayInteractor.transform.position;
            interactable.transform.rotation = rayInteractor.transform.rotation;

            // Rigidbody rb = interactable.transform.GetComponent<Rigidbody>();
            // if (rb != null)
            // {
            //     rb.velocity = Vector3.zero;
            //     rb.angularVelocity = Vector3.zero;
            // }
        }
        else
        {
            Debug.LogError("The interactor is not a ray interactor.");
        }
    }

    public void ForceDeselect(XRBaseInteractor interactor)
    {
        while (interactor.interactablesSelected.Count > 0)
        {
            SelectExit(interactor, interactor.interactablesSelected[0]);
        }
    }
}
