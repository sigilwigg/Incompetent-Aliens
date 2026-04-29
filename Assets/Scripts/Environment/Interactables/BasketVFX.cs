using Interactables;
using UnityEngine;

public class BasketVFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem baseParticles;
    public GameObject interactableObject;

    // Update is called once per frame
    void Update()
    {
        CheckPickup();
    }

    private void CheckPickup()
    {
        if (!interactableObject.GetComponent<MultiPointPickupable>().isAllPickupPointsGrabbed)
        {
            baseParticles.Play();
        }
        else
        {
            baseParticles.Stop();
        }
    }
}
