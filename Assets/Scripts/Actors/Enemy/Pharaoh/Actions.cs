using System.Collections;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        // ----- walk private state parameters -----
        private float m_waitTimer;

        #region Chase state functions

        public void ChasePlayer(Enemy.Controller controller)
        {
            controller.m_aiCore.Agent.SetDestination(controller.m_aiObserve.m_closestEnemyInView.position);
        }

        #endregion

        #region General functions

        public int PathNavigationCycle(Enemy.Controller controller, Path path, float waypointWaitTime, float waypointDistanceThreshold, int waypointIndex)
        {
            if(controller.m_aiCore.Agent.remainingDistance < waypointDistanceThreshold)
            {
                m_waitTimer += Time.deltaTime;
                if(m_waitTimer >= waypointWaitTime)
                {
                    if(waypointIndex < path.m_waypoints.Count - 1)
                        waypointIndex++;
                    else
                        waypointIndex = 0;
                    controller.m_aiCore.Agent.SetDestination(path.m_waypoints[waypointIndex].position);
                    m_waitTimer = 0f;
                }
            }
            return waypointIndex;
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
