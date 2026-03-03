using Enemy;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Observation : Enemy.AIObserve
    {
        private Blackboard m_pharaohBlackboard;

        protected override void Start()
        {
            base.Start();

            m_pharaohBlackboard = (Blackboard)m_blackboard;
        }

        override public void Observe()
        {
            base.Observe();         
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SarcophagasZone"))
            {
                m_pharaohBlackboard.m_isInSleepZone = true;
            }

            if(other.CompareTag("MirrorZone"))
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

            if(other.CompareTag("MirrorZone"))
            {
                m_pharaohBlackboard.m_isPharaohInMirrorZone = false;
            }
        }

     
    }
}
