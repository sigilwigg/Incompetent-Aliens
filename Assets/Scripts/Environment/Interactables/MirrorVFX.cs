using UnityEngine;
using Interactables;

public class MirrorVFX : MonoBehaviour
{
    // ----- Drag the mirror particles from the hierarchy into the particle system on the inspector -----
    // ----- Drag the mirror object from the hierarchy into the mirror slot on the inspector -----
    public ParticleSystem particleSystem;
    public GameObject mirror;
    public ParticleSystem sparks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        CheckPickup();
    }
    private void CheckPickup()
    {
        if (!mirror.GetComponent<Pickupable>().m_isPickedUp && !mirror.GetComponent<MirrorInMirrorZone>().m_isMirrorInPlace)
        {
            particleSystem.Play();
            sparks.Play();
        }
        else
        {
            particleSystem.Stop();
            sparks.Stop();
        }
    }
}
