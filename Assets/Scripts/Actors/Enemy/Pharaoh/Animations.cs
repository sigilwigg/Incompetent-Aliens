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
        public string CHASE = "Chase";

        private void Start()
        {
            m_enemyController = GetComponent<Enemy.Controller>();
            m_animator = GetComponent<Animator>();

            m_currentAnimationState = IDLE;
        }

        private void Update()
        {
            // ----- movement state animations -----
            switch(m_enemyController.m_aiCore.m_currentState)
            {
                case AICore.State.Idle:
                    PlayIdleAnimation();
                    break;
                case AICore.State.Walk:
                    PlayWalkingAnimation();
                    break;
                case AICore.State.Chase:
                    PlayChaseAnimation();
                    break;
                default:
                    PlayIdleAnimation();
                    break;
            }

            // ----- set rotation from input -----
            SetRotationFromInput();
        }

        private void PlayWalkingAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, WALK);
        }

        private void PlayIdleAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, IDLE);
        }

        private void PlayChaseAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, CHASE);
        }

        private void SetRotationFromInput()
        {
            m_animator.SetFloat("InputX", m_enemyController.m_aiCore.Agent.velocity.x);
            m_animator.SetFloat("InputY", m_enemyController.m_aiCore.Agent.velocity.z);
        }
    }
}
