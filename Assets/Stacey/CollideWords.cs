using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWords : MonoBehaviour
{
    public float effectDuration = 2.0f;
    [SerializeField]
    private GameObject centerEyeAnchor;

    // Reference to the HUDTextUpdater
    private HUDTextUpdater hudTextUpdater;

    private void Start()
    {
        DisablePlayOnAwake();

        // Find centerEyeAnchor if not assigned in the inspector
        if (centerEyeAnchor == null)
        {
            centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
        }

        // Find the HUDTextUpdater in the scene
        GameObject hudObject = GameObject.Find("Da_textyboi"); // Replace "HUDCanvas" with the actual name of your GameObject that contains HUDTextUpdater
        if (hudObject != null)
        {
            hudTextUpdater = hudObject.GetComponent<HUDTextUpdater>();
            if (hudTextUpdater == null)
            {
                Debug.LogError("HUDTextUpdater component not found on the HUDCanvas GameObject.");
            }
        }
        else
        {
            Debug.LogError("HUDCanvas GameObject not found.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a child of centerEyeAnchor
        if (IsChildOfCenterEyeAnchor(other.gameObject))
        {
            Debug.Log("OnTriggerEnter fired with: " + other.gameObject.name);

            // Immediately hide the object's renderers
            HideObjectRenderers(other.gameObject);

            // Start coroutine to handle the effects
            StartCoroutine(PlayEffectsAndDisable(other.gameObject));
            
            // Add a point if hudTextUpdater is assigned
            if (hudTextUpdater != null)
            {
                hudTextUpdater.AddPoints(1);
            }
            else
            {
                Debug.LogWarning("HUDTextUpdater is not assigned. Points not added.");
            }
        }
    }

    private bool IsChildOfCenterEyeAnchor(GameObject obj)
    {
        return obj.transform.IsChildOf(centerEyeAnchor.transform);
    }

    private IEnumerator PlayEffectsAndDisable(GameObject obj)
    {
        PlayParticleEffect(obj);
        PlayCollisionSound(obj);

        yield return new WaitForSeconds(effectDuration);

        DisableEntireHierarchy(obj);
    }

    private void PlayCollisionSound(GameObject obj)
    {
        AudioSource source = obj.GetComponent<AudioSource>();
        if (source != null && source.clip != null)
        {
            Debug.Log("Playing sound on: " + obj.name);
            source.Play();
        }
        else
        {
            Debug.LogWarning($"AudioSource or AudioClip is missing on the object: {obj.name}");
        }
    }

    private void PlayParticleEffect(GameObject obj)
    {
        ParticleSystem particleSystem = obj.GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            Debug.Log($"Playing particle effect on: {obj.name}");
            particleSystem.Play();
        }
        else
        {
            Debug.LogWarning($"No ParticleSystem component found on the object: {obj.name}");
        }
    }

    private void HideObjectRenderers(GameObject obj)
    {
        Debug.Log("Hiding object renderers for: " + obj.name);
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (!(renderer is ParticleSystemRenderer))
            {
                renderer.enabled = false;
            }
        }

        CanvasRenderer[] canvasRenderers = obj.GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer canvasRenderer in canvasRenderers)
        {
            canvasRenderer.gameObject.SetActive(false);
        }
    }

    private void DisableEntireHierarchy(GameObject obj)
    {
        Debug.Log("Disabling entire hierarchy for: " + obj.name);
        obj.SetActive(false);
    }

    private void DisablePlayOnAwake()
    {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            var mainModule = ps.main;
            mainModule.playOnAwake = false;
            Debug.Log("Disabling Play On Awake for ParticleSystem: " + ps.name);
        }
    }
}

