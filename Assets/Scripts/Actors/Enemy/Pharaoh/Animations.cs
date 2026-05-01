using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Animations : MonoBehaviour
    {
        private Enemy.Controller m_enemyController;
        private Animator m_animator;
        private StateMachine m_stateMachine;
        private string m_currentAnimationState;

        public string IDLE = "Idle";
        public string WALK = "Walk";
        public string CHASE = "Chase";
        public string MIRROR = "Mirror_In";
        public string STOMP = "Stomp";


        private void Start()
        {
            m_enemyController = GetComponent<Enemy.Controller>();
            m_animator = GetComponent<Animator>();
            m_stateMachine = GetComponentInChildren<StateMachine>();

            m_currentAnimationState = IDLE;
        }

        private void Update()
        {
            // ----- movement state animations -----
            switch (m_enemyController.m_aiCore.m_currentState)
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
                case AICore.State.Activity:
                    if (m_stateMachine.m_pharaohBlackboard.m_isMirrorInMirrorZone)
                        PlayMirrorAnimation();
                    else
                        PlayStompAnimation();
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

        private void PlayStompAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, STOMP);
        }

        private void PlayMirrorAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator,m_currentAnimationState, MIRROR);
        }

        private void SetRotationFromInput()
        {
            m_animator.SetFloat("InputX", m_enemyController.m_aiCore.Agent.velocity.x);
            m_animator.SetFloat("InputY", m_enemyController.m_aiCore.Agent.velocity.z);
        }
    }
}
