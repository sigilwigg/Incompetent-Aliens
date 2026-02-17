using Enemy;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class PharaohObserve : Enemy.AIObserve
    {
        public AIBlackboard m_blackboard;

        private void Start()
        {
            m_blackboard = GetComponentInParent<AIBlackboard>();
        }

        override public void Observe()
        {
            
        }
    }
}
