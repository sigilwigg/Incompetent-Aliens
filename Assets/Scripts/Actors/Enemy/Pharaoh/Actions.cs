using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        [Header("Walk State Parameters")]
        public float m_waypointWaitTime = 0.2f;
        public float m_waypointDistanceThreshold = 0.2f;

        // ----- walk private state parameters -----
        private int m_waypointIndex;
        private float m_waitTimer;


        #region Activty state functions
        public void Distracted()
        {
            //if in activity state and player held mirror
        }
        public void MadAtMissingMirror()
        {
            //if in activity state and player held mirror is false
        }
        #endregion

        #region Walk state functions

        public void WalkCycle(Enemy.Controller controller)
        {     
            if(controller.m_aiCore.Agent.remainingDistance < m_waypointDistanceThreshold)
            {
                m_waitTimer += Time.deltaTime;
                if(m_waitTimer >= m_waypointWaitTime) 
                {
                    if(m_waypointIndex < controller.m_blackboard.m_path.m_waypoints.Count - 1)
                        m_waypointIndex++;
                    else
                        m_waypointIndex = 0;
                    controller.m_aiCore.Agent.SetDestination(controller.m_blackboard.m_path.m_waypoints[m_waypointIndex].position);
                    m_waitTimer = 0f;
                }
            }
        }


        #endregion
    }
}
