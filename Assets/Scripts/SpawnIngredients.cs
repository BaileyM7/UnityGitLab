using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnIngredients : XRBaseInteractable
{
    public string ingredientPrefab = null;
    private Vector3 spawnLocation = new Vector3(-3.272f, 2.1f, -87.418f);
    public CustomInterActionManager customInteractionManager;
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    // used these to help
    // https://discussions.unity.com/t/generating-and-grabbing-an-object-all-at-once/872969/2
    // https://www.youtube.com/watch?v=H0xTz4JtWiI

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(CreateAndSelectIngredient);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(CreateAndSelectIngredient);
    }

    private void CreateAndSelectIngredient(SelectEnterEventArgs args)
    {
        if (args.interactor == null)
        {
            Debug.LogError("Interactor is null in CreateAndSelectIngredient.");
            return;
        }

        GameObject gameObj = CreateIngredient(args.interactor.transform);
        if (gameObj == null)
        {
            Debug.LogError("Failed to create ingredient. Check if the prefab path is correct and the prefab exists.");
            return;
        }

        if (customInteractionManager == null)
        {
            Debug.LogError("CustomInteractionManager is not set. Make sure it is assigned in the Inspector.");
            return;
        }

        StartCoroutine(SelectAfterDelay(gameObj, args.interactor));
    }

    private IEnumerator SelectAfterDelay(GameObject gameObj, XRBaseInteractor interactor)
    {
        yield return new WaitForEndOfFrame();

        if (gameObj.TryGetComponent(out IXRSelectInteractable interactable))
        {
            Debug.Log("Attempting to force select with: " + interactor.name);

            customInteractionManager.ForceSelect(interactor, interactable);
        }
        else
        {
            Debug.LogError("The instantiated ingredient does not have an IXRSelectInteractable component.");
        }
    }

    private GameObject CreateIngredient(Transform orientation)
    {
        GameObject objPrefab = Resources.Load<GameObject>("Pizza Stuff/" + ingredientPrefab);
        if (objPrefab == null)
        {
            Debug.LogError("Prefab not found: " + ingredientPrefab);
            return null;
        }

        GameObject ingredient = Instantiate(objPrefab, orientation.position, orientation.rotation);
        if (!ingredient.TryGetComponent(out IXRSelectInteractable interactable))
        {
            Debug.LogError("The spawned object does not have an IXRSelectInteractable component.");
        }

        return ingredient;
    }

    public void ForceDeselect()
    {
        if (leftRayInteractor != null)
        {
            customInteractionManager.ForceDeselect(leftRayInteractor);
        }
        if (rightRayInteractor != null)
        {
            customInteractionManager.ForceDeselect(rightRayInteractor);
        }
    }


    public void OnBoxClicked(string obj)
    {
        GameObject gameObject = Resources.Load<GameObject>("Pizza Stuff/" + obj);
        if (gameObject != null && gameObject.TryGetComponent(out IXRSelectInteractable interactable))
        {
            GameObject instantiatedObj = Instantiate(gameObject, spawnLocation, Quaternion.identity);

            XRBaseInteractor interactor = GetComponent<XRBaseInteractor>();
            if (interactor != null)
            {
                customInteractionManager.ForceSelect(interactor, instantiatedObj.GetComponent<IXRSelectInteractable>());
            }
            else
            {
                Debug.LogError("Interactor is null in OnBoxClicked.");
            }
        }
        else
        {
            Debug.LogError("Object not found in resources: " + obj);
        }
    }
}
