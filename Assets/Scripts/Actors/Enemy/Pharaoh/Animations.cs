using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Animations : MonoBehaviour
    {
        private Enemy.Controller m_enemyController;
        private Animator m_animator;
        private string m_currentAnimationState;

        public string IDLE = "Idle";
        public string WALK = "Walk";

        private void Start()
        {
            m_enemyController = GetComponent<Enemy.Controller>();
            m_animator = GetComponent<Animator>();

            m_currentAnimationState = IDLE;
        }
    }
}
