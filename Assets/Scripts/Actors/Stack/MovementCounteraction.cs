using UnityEngine;
using System.Collections.Generic;

/*
 * Script in progress.
 */

public class MovementCounteraction : MonoBehaviour
{
    private Stack.Controller m_stackController;
    public List<MatchHalfWobble> m_positions = new List<MatchHalfWobble>();
    public List<float> m_initialPercentages = new List<float>();

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
            if(distanceFromCurrent < 0.1f)
            {
                m_positions[idx].m_percentageMatch = 0.0f;
            } else
            {
                m_positions[idx].m_percentageMatch = m_initialPercentages[idx];
            }
        }
    }

    private void GetInitialMatchPercentages()
    {
        int idx = 0;
        foreach (MatchHalfWobble position in m_positions)
        {
            m_initialPercentages[idx] = position.m_percentageMatch;
            idx++;
        }
    }
}
