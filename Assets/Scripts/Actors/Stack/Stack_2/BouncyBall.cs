using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private bool m_shouldPropelSelf = false;
    private Vector3 m_propelDirection;
    private float m_propelForce;

    private void FixedUpdate()
    {
        if(m_shouldPropelSelf) PropelSelf();
    }

    public void SetPropelSelf(Vector3 direction, float force)
    {
        m_shouldPropelSelf = true;
        m_propelDirection = direction;
        m_propelForce = force;
    }
    private void PropelSelf()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(
            m_propelDirection * m_propelForce,
            ForceMode.Impulse
        );

        Destroy(gameObject, 3.0f);
    }
}
