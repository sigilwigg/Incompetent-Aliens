
using System.Runtime.CompilerServices;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    public Vector3 m_originalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            particleSystem.Play();
            transform.position = m_originalPosition;
        }
    }
}

