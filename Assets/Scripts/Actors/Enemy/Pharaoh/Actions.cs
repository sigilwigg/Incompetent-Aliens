using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        // ----- walk private state parameters -----
        private float m_waitTimer;

        private Observation m_observation;
        private StateMachine m_stateMachine;

        private void Start()
        {
            m_observation = GetComponent<Observation>();
            m_stateMachine = GetComponent<StateMachine>();
        }

        #region Chase state functions

        public void ChasePlayer()
        {
            m_stateMachine.Agent.SetDestination(m_observation.m_closestEnemyInView.position);
        }

        //public void throwPlayer(Enemy.Controller controller)
        //{

        //}

        #endregion

        #region General functions

        public int PathNavigationCycle(Path path, float waypointWaitTime, float waypointDistanceThreshold, int waypointIndex)
        {
            if (m_stateMachine.Agent.remainingDistance < waypointDistanceThreshold)
            {
                m_waitTimer += Time.deltaTime;
                if (m_waitTimer >= waypointWaitTime)
                {
                    if (waypointIndex < path.m_waypoints.Count - 1)
                        waypointIndex++;
                    else
                        waypointIndex = 0;
                    m_stateMachine.Agent.SetDestination(path.m_waypoints[waypointIndex].position);
                    m_waitTimer = 0f;
                }
            }
            return waypointIndex;
        }

        public void ChangeSpeed(float newSpeed)
        {
            m_stateMachine.Agent.speed = newSpeed;
        }

        public void ChangeVisionAngle(float newVisionAngle)
        {
            m_observation.m_visionAngle = newVisionAngle;
        }

        public void ChangeVisionRange(float newVisionRange)
        { 
            m_observation.m_visionRange = newVisionRange;
        }

        public void ChangeCatchRange(float newCatchRange)
        {
            m_observation.m_catchRange = newCatchRange;
        }

        #endregion

    }
}
