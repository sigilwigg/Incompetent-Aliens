using Unity.VisualScripting;
using UnityEngine;

public class GlyphBreak : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject floor;
    public GameObject glyphsprite;
    public Vector3 originalLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalLocation = transform.position;
    }

    void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.layer == 3)
        {
            glyphsprite.GetComponent<Renderer>().enabled = false;
            particleSystem.Play();

            Invoke("RespawnGlyph", 2);
        }
    }

    private void RespawnGlyph()
    {
        transform.position = originalLocation;
        glyphsprite.GetComponent<Renderer>().enabled = true;
    }
}
