using System.Collections;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        // ----- walk private state parameters -----
        private int m_waypointIndex;
        private float m_waitTimer;

        #region sleep state functions


        #endregion

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



        #endregion

        #region Chase state functions

        public void ChasePlayer(Enemy.Controller controller)
        {
            controller.m_aiCore.Agent.SetDestination(controller.m_aiObserve.m_closestEnemyInView.position);
        }

        #endregion

        #region General functions

        public void PathNavigationCycle(Enemy.Controller controller, Path path, float waypointWaitTime, float waypointDistanceThreshold)
        {
            if (controller.m_aiCore.Agent.remainingDistance < waypointDistanceThreshold)
            {
                m_waitTimer += Time.deltaTime;
                if (m_waitTimer >= waypointWaitTime)
                {
                    if (m_waypointIndex < path.m_waypoints.Count - 1)
                        m_waypointIndex++;
                    else 
                        m_waypointIndex = 0;
                    controller.m_aiCore.Agent.SetDestination(path.m_waypoints[m_waypointIndex].position);
                    m_waitTimer = 0f;
                }
            }
        }

        public void ChangeSpeed(Enemy.Controller controller, float newSpeed)
        {
            controller.m_aiCore.Agent.speed = newSpeed;
        }

        public void ChangeVisionAngle(Enemy.Controller controller, float newVisionAngle)
        {
            controller.m_aiObserve.m_visionAngle = newVisionAngle;
        }

        public void ChangeVisionRange(Enemy.Controller controller, float newVisionRange)
        {
            controller.m_aiObserve.m_visionRange = newVisionRange;
        }

        #endregion

    }
}
