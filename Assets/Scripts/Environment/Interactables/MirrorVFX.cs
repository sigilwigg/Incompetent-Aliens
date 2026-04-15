using UnityEngine;
using Interactables;

public class MirrorVFX : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject mirror;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPickup();
    }

    private void CheckPickup()
    {
        if (mirror.GetComponent<StackPickupable>().m_isPickedUp)
        {
            PlayParticles();
        }
    }
    private void PlayParticles()
    {
        particleSystem.Play();
    }
}
