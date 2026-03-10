using Enemy;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Observation : Enemy.AIObserve
    {
        private Blackboard m_pharaohBlackboard;

        [Header("Pharaoh LOS")]
        public float m_catchRange;

        protected override void Start()
        {
            base.Start();

            m_pharaohBlackboard = (Blackboard)m_blackboard;
        }

        override public void Observe()
        {
            base.Observe();
            canCatchPlayer();
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

        private void canCatchPlayer()
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, m_catchRange, m_targetMask);

            m_pharaohBlackboard.m_canCatchPlayer = false;

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
                        m_pharaohBlackboard.m_canCatchPlayer = true;
                    }
                }

            }
        }

    }
}
