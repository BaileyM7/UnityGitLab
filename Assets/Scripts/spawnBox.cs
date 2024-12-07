using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShelfPizzaSpawner : XRGrabInteractable
{
    [Header("Pizza Box Settings")]
    [Tooltip("The pizza box prefab to spawn when the shelf is grabbed.")]
    public GameObject pizzaBoxPrefab;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args); // Ensure XRGrabInteractable functionality works

        // Ensure we have a pizza box prefab
        if (pizzaBoxPrefab == null)
        {
            Debug.LogWarning("No pizzaBoxPrefab assigned on " + gameObject.name);
            return;
        }

        // Get the interactor (the hand/controller that grabbed the shelf)
        IXRSelectInteractor interactor = args.interactorObject;
        if (interactor == null)
        {
            Debug.LogWarning("No interactor found for OnSelectEntered on shelf.");
            return;
        }

        // Find the attachment point of the interactor to spawn the pizza box at that location
        Transform attachTransform = interactor.GetAttachTransform(this);

        // Instantiate the pizza box at the interactor’s attach transform
        GameObject spawnedPizzaBox = Instantiate(pizzaBoxPrefab, attachTransform.position, attachTransform.rotation);

        // Make the interactor grab the new pizza box
        XRGrabInteractable pizzaBoxInteractable = spawnedPizzaBox.GetComponent<XRGrabInteractable>();
        if (pizzaBoxInteractable != null)
        {
            // Use SelectEnter to grab the pizza box with the interactor
            interactionManager.SelectEnter(interactor, pizzaBoxInteractable);
        }

        // Make sure the shelf is not removed or deactivated
        // Optional: If you don't want the shelf to be "grabbed" anymore, use this to release the shelf without destroying it
        interactionManager.SelectExit(interactor, this);
    }
}
