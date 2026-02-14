using UnityEngine;

public class MatchHalfWobble: MonoBehaviour
{
    public Transform m_stackCenter;
    public Transform m_targetTransform;
    [Range(0.0f, 1.0f)]
    public float m_percentageMatch = 1.0f;
    public float m_matchSpeed = 1.0f;

    private Vector3 m_currentPosition;
    private Vector3 m_targetPosition;

    private void Update()
    {
        Vector3 center = new Vector3(m_stackCenter.position.x, transform.position.y, m_stackCenter.position.z);
        m_targetPosition = Vector3.Lerp(center, m_targetTransform.position, m_percentageMatch);
        transform.position = Vector3.Lerp(transform.position, m_targetPosition, Time.deltaTime * m_matchSpeed);
    }
}
