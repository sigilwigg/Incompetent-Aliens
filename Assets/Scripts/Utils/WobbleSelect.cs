using UnityEngine;

public class WobbleSelect : MonoBehaviour
{
    public Transform m_targetTransform;

    private bool m_timeToSelectNewWobble = true;
    public float m_cooldown;
    public float m_matchSpeed;

    private Vector3 m_targetPosition;

    private void Update()
    {
        if(m_timeToSelectNewWobble)
        {
            m_timeToSelectNewWobble = false;
            SelectNewWobble();
            Invoke(nameof(ResetSelectNewWobble), UnityEngine.Random.Range(0.0f, m_cooldown));
        }

        transform.position = Vector3.Lerp(transform.position, m_targetPosition, Time.deltaTime * m_matchSpeed);
    }

    private void SelectNewWobble()
    {
        m_targetPosition = m_targetTransform.position;
    }

    private void ResetSelectNewWobble()
    {
        m_timeToSelectNewWobble = true;
    }
}
