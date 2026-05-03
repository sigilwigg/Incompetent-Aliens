using UnityEngine;

/*
 *  Handles functionality for breaking the glyph when it touches the ground, catchign the glyph in the basket,
 *  and locking the glyph in place in the Sarcophogus.
 */

public class GlyphBreak : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject glyphsprite;
    public Vector3 originalLocation;
    public bool hasBeenCaught;
    private Rigidbody rb;
    
    void Start()
    {
        // ----- Declare the original position of the glyph so that it can reset -----
        originalLocation = transform.position;

        // ----- Declare the rigidbody variable so that we can freeze the position later -----
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // ----- handle ground-glyph collision -----
        if (collision.gameObject.layer == 3 && collision.gameObject.tag == "Ground")
        {
            if (hasBeenCaught) return;

            glyphsprite.GetComponent<Renderer>().enabled = false;
            particleSystem.Play();

            Invoke("RespawnGlyph", 2);
        }

        // ----- handle basket-glyph interaction -----
        if (collision.gameObject.tag == "Basket")
        {
            rb.constraints = RigidbodyConstraints.None;
            Invoke("FreezeGlyph", 1);
        }
    }

    private void RespawnGlyph()
    {
        // ----- respawn and reset -----
        transform.position = originalLocation;
        glyphsprite.GetComponent<Renderer>().enabled = true;
    }

    private void FreezeGlyph()
    {
        // ----- Freeze glyph in sarcophagus -----
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // ----- turn off breakability -----
        hasBeenCaught = true;
    }
}
