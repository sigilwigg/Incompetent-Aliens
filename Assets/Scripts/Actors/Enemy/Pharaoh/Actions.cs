using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        // ----- walk private state parameters -----
        private float m_pathingWaitTimer;

        private float m_throwCoolDownTimer;

        private Observation m_observation;
        private StateMachine m_stateMachine;

        //----- prefab references -----
        public GameObject m_bouncingBallPrefab;
        public GameObject m_dustUpParticle;

        private void Start()
        {
            m_observation = GetComponent<Observation>();
            m_stateMachine = GetComponent<StateMachine>();
        }

        #region Chase state functions

        private void Update()
        {
            if (m_throwCoolDownTimer > 0)
            {
                m_throwCoolDownTimer -= Time.deltaTime;
            }
        }

        public void ChasePlayer()
        {
            m_stateMachine.Agent.SetDestination(m_observation.m_closestEnemyInView.position);
        }

        public void throwPlayer(float waitTimer, float bounceForce = 0.5f)
        {
            if (m_throwCoolDownTimer <= 0)
            {
                Instantiate(m_dustUpParticle, transform.position, Quaternion.identity);

                int RandomfallDirectionX = Random.Range(-1, 2); //returns ints -1, 0 and 1
                int RandomfallDirectionY = Random.Range(-1, 2); //returns ints -1, 0 and 1
                if ((RandomfallDirectionX == 0) && (RandomfallDirectionY == 0)) RandomfallDirectionX = 1;

                GameObject bouncePositioner = Instantiate(m_bouncingBallPrefab, transform.position, Quaternion.identity);
                BouncyBall bouncyBall = bouncePositioner.GetComponent<BouncyBall>();
                bouncyBall.SetPropelSelf(new Vector3(0.5f * RandomfallDirectionX, 0.2f, 0.5f * RandomfallDirectionY), bounceForce);

                Player.Controller playerController = m_observation.m_closestEnemyInCatchView;
                playerController.SetBeingThrown(bouncePositioner.transform);

                m_throwCoolDownTimer = waitTimer;
            }
        }

        #endregion

        #region General functions

        public int PathNavigationCycle(Path path, float waypointWaitTime, float waypointDistanceThreshold, int waypointIndex)
        {
            if (m_stateMachine.Agent.remainingDistance < waypointDistanceThreshold)
            {
                m_pathingWaitTimer += Time.deltaTime;
                if (m_pathingWaitTimer >= waypointWaitTime)
                {
                    if (waypointIndex < path.m_waypoints.Count - 1)
                        waypointIndex++;
                    else
                        waypointIndex = 0;
                    m_stateMachine.Agent.SetDestination(path.m_waypoints[waypointIndex].position);
                    m_pathingWaitTimer = 0f;
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
