using UnityEngine;

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
        // ----- Detect a collision with layer 3 (ground) -----
        if (collision.gameObject.layer == 3 && collision.gameObject.tag == "Ground")
        {
            // ----- Ends the if statement if the glyph has already been caught in the basket -----
            if (hasBeenCaught) return;

            // ----- Hides the glyph sprite and plays the break effect -----
            glyphsprite.GetComponent<Renderer>().enabled = false;
            particleSystem.Play();

            // ------ Plays the respawn glyph function on a 2 second delay -----
            Invoke("RespawnGlyph", 2);
        }

        // ----- Detects a collision inside the basket tag -----
        if (collision.gameObject.tag == "Basket")
        {
            // ----- Lets the glyph roll around inside the basket for a second -----
            rb.constraints = RigidbodyConstraints.None;

            // ----- Plays the freeze glyph function a second after it hits the basket -----
            Invoke("FreezeGlyph", 1);
        }
    }

    private void RespawnGlyph()
    {
        // ----- Sets the glyph back to the original position and shows the sprite -----
        transform.position = originalLocation;
        glyphsprite.GetComponent<Renderer>().enabled = true;
    }

    private void FreezeGlyph()
    {
        // ----- Freezes every aspect of the glyph so that it doesn't fall after being placed in the sarcophagus -----
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // ----- Ensures that the glyph won't break if it hits the ground -----
        hasBeenCaught = true;
    }
}
