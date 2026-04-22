using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Actions : MonoBehaviour
    {
        // ----- timer vars -----
        private float m_pathingWaitTimer;
        private float m_throwCoolDownTimer;

        //----- throw var -----
        public bool m_isThrowing = false;

        //----- on object refs ------
        private Observation m_observation;
        private StateMachine m_stateMachine;
        public GameObject m_pharaohModel;

        //----- prefab references -----
        public GameObject m_bouncingBallPrefab;
        public GameObject m_dustUpParticle;

        //----- time manager ref -----
        public TimeManager m_timeManager;

        private void Start()
        {
            m_observation = GetComponent<Observation>();
            m_stateMachine = GetComponent<StateMachine>();
        }

        private void Update()
        {
            if (m_throwCoolDownTimer > 0)
            {
                m_throwCoolDownTimer -= m_timeManager.deltaTime;
            }
        }

        #region Chase state functions

        public void ChasePlayer()
        {
            m_stateMachine.Agent.SetDestination(m_observation.m_closestEnemyInView.position);
        }

        public IEnumerator ThrowPlayerCoroutine(float waitTimer, float bounceForce = 0.5f)
        {
            if (m_throwCoolDownTimer <= 0)
            {
                m_isThrowing = true;

                //----- spawn dust up particle ----
                GameObject dustUpGO = Instantiate(m_dustUpParticle, transform.position, Quaternion.identity);
                ParticleSystem ps = dustUpGO.GetComponent<ParticleSystem>();

                float waitTime = ps.main.duration + ps.main.startLifetime.constantMax;

                Destroy(dustUpGO, waitTime);

                //----- get references to player caught and player model -----
                Player.Controller playerController = m_observation.m_closestEnemyInCatchView;
                GameObject playerModel = playerController.m_playerModel;

                //----- disable models and movement -----
                playerController.m_canMove = false;
                playerModel.SetActive(false);         
                m_pharaohModel.SetActive(false);
                m_stateMachine.Agent.speed = 0f;

                yield return new WaitForSeconds(waitTime - 1.5f);

                //----- renable models and movement -----
                playerController.m_canMove = true;
                playerModel.SetActive(true);
                m_pharaohModel.SetActive(true);
                m_stateMachine.Agent.speed = m_stateMachine.m_walkSpeed;


                //----- throw player -----
                int RandomfallDirectionX = Random.Range(-1, 2); //returns ints -1, 0 and 1
                int RandomfallDirectionY = Random.Range(-1, 2); //returns ints -1, 0 and 1
                if ((RandomfallDirectionX == 0) && (RandomfallDirectionY == 0)) RandomfallDirectionX = 1;

                GameObject bouncePositioner = Instantiate(m_bouncingBallPrefab, transform.position, Quaternion.identity);
                BouncyBall bouncyBall = bouncePositioner.GetComponent<BouncyBall>();
                bouncyBall.SetPropelSelf(new Vector3(0.5f * RandomfallDirectionX, 0.2f, 0.5f * RandomfallDirectionY), bounceForce);

                playerController.SetBeingThrown(bouncePositioner.transform);

                m_throwCoolDownTimer = waitTimer;

                m_isThrowing = false;
            }
        }

        #endregion

        #region General functions

        public int PathNavigationCycle(Path path, float waypointWaitTime, float waypointDistanceThreshold, int waypointIndex)
        {
            if (m_stateMachine.Agent.remainingDistance < waypointDistanceThreshold)
            {
                m_pathingWaitTimer += m_timeManager.deltaTime;
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
