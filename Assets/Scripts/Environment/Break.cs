
using System.Runtime.CompilerServices;
using UnityEngine;

public class Break : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_particleSystem;
    public Vector3 m_originalPosition;
    public Vector3 m_placeholder;

    public float m_timer = 0f;
    public float m_delay = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer > 0f) 
        { 
            m_timer -= Time.deltaTime;
            if (m_timer > 0f && m_timer < (m_delay - 0.2f))
            {
                transform.position = m_placeholder;
            }
            else if (m_timer < 0f)
            {
                transform.position = m_originalPosition;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            m_particleSystem.Play();
            m_timer = m_delay;
        }
    }
}

