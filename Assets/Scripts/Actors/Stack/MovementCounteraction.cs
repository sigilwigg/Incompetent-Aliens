using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
 * Script in progress.
 */

public class MovementCounteraction : MonoBehaviour
{
    private Stack.Controller m_stackController;
    public List<MatchHalfWobble> m_positions = new List<MatchHalfWobble>();
    public List<float> m_initialPercentages = new List<float>();

    public float m_distanceTolerance = 0.15f;
    public float m_alignmentDuration = 1.5f;
    public float m_alignmentSpeed = 0.25f;
    public float m_influenceMultiplier = 2.0f;

    private IEnumerator m_currentCoroutine;

    private void Start()
    {
        m_stackController = GetComponent<Stack.Controller>();
        GetInitialMatchPercentages();
    }

    private void Update()
    {
        float currentRotation = m_stackController.m_playerControllers[0].m_rotation;

        for(int idx = 0; idx < m_positions.Count; idx++)
        {
            float positionRotation = Mathf.Atan2(m_positions[idx].transform.localPosition.x, m_positions[idx].transform.localPosition.z);
            positionRotation *= Mathf.Rad2Deg;

            float distanceFromCurrent = currentRotation - positionRotation;
            Debug.Log(distanceFromCurrent.ToString());
            distanceFromCurrent /= 180.0f;

            Debug.Log(distanceFromCurrent.ToString());
            if(distanceFromCurrent < m_distanceTolerance)
            {
                Debug.Log("shorter");
                m_currentCoroutine = IsAlignedOperations();
                StartCoroutine(m_currentCoroutine);
            } else
            {
                Debug.Log("longer");
                m_currentCoroutine = IsNotAlignedOperations();
                StartCoroutine(m_currentCoroutine);
            }
        }
    }

    IEnumerator IsAlignedOperations()
    {
        float timeElapsed = 0.0f;
        float targetPercentage = 0.0f;

        while (timeElapsed < m_alignmentDuration)
        {
            for (int idx = 0; idx < m_stackController.m_playersInStack; idx++)
            {
                m_positions[idx].m_percentageMatch = Mathf.Lerp(
                    m_initialPercentages[idx],
                    -m_initialPercentages[idx],
                    Time.deltaTime * m_alignmentSpeed
                );
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        for (int idx = 0; idx < m_stackController.m_playersInStack; idx++)
        {
            m_positions[idx].m_percentageMatch = -m_initialPercentages[idx];
        }
    }

    IEnumerator IsNotAlignedOperations()
    {
        float timeElapsed = 0.0f;
        float[] currentPercentages = new float[4];

        for (int idx = 0; idx < m_stackController.m_playersInStack; idx++)
        {
            currentPercentages[idx] = m_positions[idx].m_percentageMatch;
        }

        while (timeElapsed < m_alignmentDuration)
        {
            for (int idx = 0; idx < m_stackController.m_playersInStack; idx++)
            {
                m_positions[idx].m_percentageMatch = Mathf.Lerp(
                    currentPercentages[idx],
                    m_initialPercentages[idx],
                    Time.deltaTime * m_alignmentSpeed
                );
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        for (int idx = 0; idx < m_stackController.m_playersInStack; idx++)
        {
            m_positions[idx].m_percentageMatch = m_initialPercentages[idx];
        }
    }

    private void GetInitialMatchPercentages()
    {
        int idx = 0;
        foreach (Transform positionTransform in m_stackController.m_stackPositionTransforms)
        {
            m_positions[idx] = positionTransform.GetComponent<MatchHalfWobble>();
            m_initialPercentages[idx] = m_positions[idx].m_percentageMatch;
            idx++;
        }
    }
}
