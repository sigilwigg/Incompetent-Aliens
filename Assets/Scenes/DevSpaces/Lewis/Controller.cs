using UnityEngine;

namespace Lewis
{
    public class Controller : MonoBehaviour
    {
        private Lewis.EnemyManager m_manager;

        public bool m_canMove;

        private void Awake()
        {
            m_manager = GetComponentInChildren<Lewis.EnemyManager>();
        }
    }
}