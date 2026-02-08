using UnityEngine;

namespace Enemy
{
    public class Controller : MonoBehaviour
    {
        public bool m_canMove = true;

        public AIBlackboard m_blackboard;

        private void Start()
        {
            m_blackboard = GetComponent<AIBlackboard>();
        }
    }
}