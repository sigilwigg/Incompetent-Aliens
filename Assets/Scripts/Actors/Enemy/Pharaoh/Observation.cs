using Enemy;
using Interactables;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Observation : Enemy.AIObserve
    {
        private Blackboard m_pharaohBlackboard;

        [Header("Pharaoh LOS")]
        public float m_catchRange;

        public Player.Controller m_closestEnemyInCatchView;
        public MirrorInMirrorZone m_mirrorInMirrorZone;



        protected override void Start()
        {
            base.Start();

            m_pharaohBlackboard = (Blackboard)m_blackboard;
            m_mirrorInMirrorZone = FindFirstObjectByType<MirrorInMirrorZone>();
        }

        override public void Observe()
        {
            base.Observe();
            CanCatchPlayer();
            IsMirrorInZone();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SarcophagasZone"))
            {
                m_pharaohBlackboard.m_isInSleepZone = true;
            }

            if (other.CompareTag("MirrorZone"))
            {
                m_pharaohBlackboard.m_isPharaohInMirrorZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("SarcophagasZone"))
            {
                m_pharaohBlackboard.m_isInSleepZone = false;
            }

            if (other.CompareTag("MirrorZone"))
            {
                m_pharaohBlackboard.m_isPharaohInMirrorZone = false;
            }
        }

        private void CanCatchPlayer()
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, m_catchRange, m_targetMask);

            m_pharaohBlackboard.m_canCatchPlayer = false;
            m_closestEnemyInCatchView = null;

            float minSqrDistance = Mathf.Infinity;

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                float sqrDistToTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDistToTarget < minSqrDistance)
                {
                    if (!Physics.Raycast(transform.position, dirToTarget, Mathf.Sqrt(sqrDistToTarget), m_obstacleMask))
                    {
                        minSqrDistance = sqrDistToTarget;
                        m_closestEnemyInCatchView = target.gameObject.GetComponentInParent<Player.Controller>();


                        m_pharaohBlackboard.m_canCatchPlayer = true;
                    }
                }

            }
        }

        private void IsMirrorInZone()
        {
           if(m_mirrorInMirrorZone.m_isMirrorInPlace)
            {
                m_pharaohBlackboard.m_isMirrorInMirrorZone = true;
            }
            else
            {
                m_pharaohBlackboard.m_isMirrorInMirrorZone = false;
            }
           
        }
    }
}
