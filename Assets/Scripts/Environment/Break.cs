
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            particles.gameObject.ParticleSystem.Play;
            Debug.Log("A");
        }
    }
}

