using UnityEngine;

public class ToPositionLerpCurve : MonoBehaviour
{
    public AnimationCurve m_curve;
    public float m_sample;

    public Transform m_targetTransform;
    public Vector3 m_targetPrevPos;
    public bool m_isTargetMoving;
    public float m_matchSpeed = 1.0f;

    private Vector3 m_currentPosition;
    private Vector3 m_targetPosition;
    private Vector3 m_prevPos;

    private bool m_isBounceMove = false;
    private float m_elapsed;

    private void Start()
    {
        m_currentPosition = transform.position;
    }

    private void Update()
    {
        m_targetPosition = m_targetTransform.position;
        if(m_targetPosition != m_targetPrevPos)
        {
            m_targetPrevPos = m_targetPosition;
            m_isTargetMoving = true;
        } else
        {
            m_isTargetMoving = false;
        }

        
        if (m_isTargetMoving)
        {
            m_isBounceMove = false;
            m_currentPosition = Vector3.Lerp(transform.position, m_targetPosition, Time.deltaTime * m_matchSpeed);
        } else
        {
            float time = Vector3.Distance(m_targetPosition, transform.position);
            if (!m_isBounceMove)
            {
                m_elapsed = 0.0f;
                m_sample = 0.0f;
                m_isBounceMove = true;
            }

            m_elapsed += Time.deltaTime;
            m_sample = m_elapsed / time;
            m_currentPosition = Vector3.Lerp(transform.position, m_targetPosition, m_curve.Evaluate(m_sample));
        }

        
        transform.position = m_currentPosition;
    }
}
