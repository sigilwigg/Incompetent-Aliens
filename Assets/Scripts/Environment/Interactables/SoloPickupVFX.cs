using UnityEngine;
using Interactables;

public class SoloPickupVFX : MonoBehaviour
{
    public ParticleSystem baseParticles;
    public ParticleSystem sparks;
    public GameObject interactableObject;

    // Update is called once per frame
    void Update()
    {
        CheckPickup();
    }

    private void CheckPickup()
    {
        if (!interactableObject.GetComponent<Pickupable>().m_isPickedUp)
        {
            baseParticles.Play();
        }
        else
        {
            baseParticles.Stop();
        }
    }
}
