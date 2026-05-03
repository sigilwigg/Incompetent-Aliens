using UnityEngine;
using Interactables;

/*
 *  Plays VFX effect when a pickupable item is picked up.
 */

public class SoloPickupVFX : MonoBehaviour
{
    public ParticleSystem baseParticles;
    public GameObject interactableObject;

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
