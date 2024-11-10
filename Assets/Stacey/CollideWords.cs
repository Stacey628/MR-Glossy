using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWords : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Disable the entire hierarchy of the collided 3D object
        DisableObjectHierarchy(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Disable the entire hierarchy of the collided UI object
        DisableObjectHierarchy(other.gameObject);
    }

    private void DisableObjectHierarchy(GameObject obj)
    {
        // Disable all renderers and UI elements in this object and its children
        SetActiveStatusForAllRenderers(obj, false);
    }

    private void SetActiveStatusForAllRenderers(GameObject obj, bool status)
    {
        // Handle 3D renderers
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = status;
        }

        // Handle UI (2D) canvas renderers
        CanvasRenderer[] canvasRenderers = obj.GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer canvasRenderer in canvasRenderers)
        {
            canvasRenderer.gameObject.SetActive(status);
        }
    }
}
