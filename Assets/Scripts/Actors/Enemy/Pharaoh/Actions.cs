using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        [Header("Walk State Parameters")]
        public int m_waypointIndex;
        public float m_waitTimer;


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
            if(controller.m_aiCore.Agent.remainingDistance < 0.2f)
            {
                m_waitTimer += Time.deltaTime;
                if(m_waitTimer >= 3f) // edit wait time here.
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
