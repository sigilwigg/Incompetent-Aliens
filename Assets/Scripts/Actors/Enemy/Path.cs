using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Enemy
{
    public class Path
    {
        public List<Transform> m_waypoints;

        [SerializeField] private bool m_AlwaysDrawPath;
        [SerializeField] private bool m_DrawAsLoop;
        [SerializeField] private bool m_DrawNumbers;

        public Color debugColour = Color.white;

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if(m_AlwaysDrawPath)
            {
                DrawPath();
            }
        }
        // ----- draws the path in the editor -----
        public void DrawPath()
        {
            for(int i = 0;i < m_waypoints.Count;i++)
            {
                GUIStyle lableStyle = new GUIStyle();
                lableStyle.fontSize = 30;
                lableStyle.normal.textColor = debugColour;
                if(m_DrawNumbers)
                    Handles.Label(m_waypoints[i].position, i.ToString(), lableStyle);
                // ----- draw lines between waypoints -----
                if(i >= 1)
                {
                    Gizmos.color = debugColour;
                    Gizmos.DrawLine(m_waypoints[i - 1].position, m_waypoints[i].position);

                    if(m_DrawAsLoop)
                        Gizmos.DrawLine(m_waypoints[m_waypoints.Count - 1].position, m_waypoints[0].position);
                }
            }
        }

        public void OnDrawGizmosSelected()
        {
            if(m_AlwaysDrawPath)
                return;
            else
                DrawPath();
        }

#endif
    }
}
