using Interactables;
using UnityEngine;

public class BasketVFX : MonoBehaviour
{
    public ParticleSystem baseParticles;
    public GameObject interactableObject;

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
