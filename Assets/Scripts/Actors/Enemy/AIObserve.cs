using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class AIObserve : MonoBehaviour
    {
        [Header("LOS Settings")]
        public float m_visionRange;
        [Range(0, 360)]
        public float m_visionAngle;

        [Header("Layer Masks")]
        public LayerMask m_targetMask;
        public LayerMask m_obstacleMask;

        [Header("References")]
        public AIBlackboard m_blackboard;

        private void Start()
        {
            m_blackboard = GetComponent<AIBlackboard>();

            StartCoroutine(ObserveRoutine());
        }

        //----- Check for Players every 0.2 seconds -----
        private IEnumerator ObserveRoutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(0.2f);
                Observe();
            }
        }

        // ----- Check for Players in vision range and angle, and if there are any obstacles in the way -----
        public virtual void Observe()
        {

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, m_visionRange, m_targetMask);

            if(targetsInViewRadius.Length > 0)
            {
                for(int i = 0;i < targetsInViewRadius.Length;i++)
                {
                    Transform target = targetsInViewRadius[i].transform;
                    Vector3 dirToTarget = (target.position - transform.position).normalized;

                    if(Vector3.Angle(transform.forward, dirToTarget) < m_visionAngle / 2)
                    {
                        float distToTarget = Vector3.Distance(transform.position, target.position);

                        if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, m_obstacleMask))
                            m_blackboard.m_canSeePlayer = true;
                        else
                            m_blackboard.m_canSeePlayer = false;
                    }
                    else
                        m_blackboard.m_canSeePlayer = false;
                }
            }
            else if(m_blackboard.m_canSeePlayer)            
                m_blackboard.m_canSeePlayer = false;
            
        }
    }
}
