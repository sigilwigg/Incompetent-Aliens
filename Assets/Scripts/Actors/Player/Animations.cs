using UnityEngine;

/*
 *  Controls the animations for the player based on player moveState and input.
 *  
 *  PlayIdleAnimation()         =>  Calls for idle aniamtion blend tree state;
 *  PlayWalkAnimation()         =>  Calls for walk aniamtion blend tree state;
 *  SetRotationFromInput()      =>  Takes -1 - 1 input values for x y and sets in animator;
 */

namespace Player
{
    public class Animations : MonoBehaviour
    {
        private Player.Controller m_playerController;
        private Animator m_animator;
        private string m_currentAnimationState;

        public string IDLE = "Idle";
        public string WALK = "Walk";

        private void Start()
        {
            m_playerController = GetComponent<Player.Controller>();
            m_animator = GetComponent<Animator>();

            m_currentAnimationState = IDLE;
            m_animator.SetFloat("InputX", m_playerController.m_input.x);
            m_animator.SetFloat("InputY", m_playerController.m_input.y);
        }

        private void Update()
        {
            // ----- movement state animations -----
            switch (m_playerController.m_moveState)
            {
                case Controller.MoveState.Idle:
                    PlayIdleAnimation();
                    break;
                case Controller.MoveState.Walking:
                    PlayWalkingAnimation();
                    break;
                default:
                    PlayIdleAnimation();
                    break;
            }

            // ----- rotation -----
            SetRotationFromInput();
        }

        private void PlayIdleAnimation()
        {
            AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, IDLE);
        }

        private void PlayWalkingAnimation()
        {
            AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, WALK);
        }

        private void SetRotationFromInput()
        {
            if (m_playerController.m_input != Vector2.zero)
            {
                m_animator.SetFloat("InputX", m_playerController.m_input.x);
                m_animator.SetFloat("InputY", m_playerController.m_input.y);
            } 
        }

    }
}
