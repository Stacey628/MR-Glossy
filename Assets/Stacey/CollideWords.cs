using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWords : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure there's an AudioSource component attached
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found. Please attach one with an audio clip.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Play sound effect
        PlayCollisionSound();

        // Disable the entire hierarchy of the collided 3D object
        DisableObjectHierarchy(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Play sound effect
        PlayCollisionSound();

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

    private void PlayCollisionSound()
    {
        // Check if the audioSource and clip are ready
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing.");
        }
    }
}
