using UnityEngine;

namespace Enemy
{
    public class Controller : MonoBehaviour
    {
        public bool m_canMove = true;

        public AIBlackboard m_blackboard;
        public AICore m_aiCore;
        public AIObserve m_aiObserve;

        private void Start()
        {
            m_blackboard = GetComponent<AIBlackboard>();
            m_aiCore = GetComponentInChildren<AICore>();
            m_aiObserve = GetComponentInChildren<AIObserve>();
        }

        private void Update()
        {
            m_aiObserve.Observe();
            m_aiCore.Decide();
            m_aiCore.Evaluate();
        }
    }
}