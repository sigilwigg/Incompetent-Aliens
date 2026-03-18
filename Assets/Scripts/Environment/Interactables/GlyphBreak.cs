using Unity.VisualScripting;
using UnityEngine;

public class GlyphBreak : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject floor;
    public GameObject glyphsprite;
    public Vector3 originalLocation;
    public bool hasBeenCaught;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalLocation = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (hasBeenCaught) return;

            glyphsprite.GetComponent<Renderer>().enabled = false;
            particleSystem.Play();

            Invoke("RespawnGlyph", 2);
        }

        if (collision.gameObject.tag == "Basket")
        {
            rb.constraints = RigidbodyConstraints.None;

            Invoke("FreezeGlyph", 1);
        }
    }

    private void RespawnGlyph()
    {
        transform.position = originalLocation;
        glyphsprite.GetComponent<Renderer>().enabled = true;
    }

    private void FreezeGlyph()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;

        hasBeenCaught = true;
    }
}
