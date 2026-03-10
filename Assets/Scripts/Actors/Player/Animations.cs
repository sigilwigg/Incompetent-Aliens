using UnityEngine;

/*
 *  Controls the animations for the player based on player moveState and input.
 *  
 *  PlayIdleAnimation()         =>  Calls for idle aniamtion blend tree state;
 *  PlayWalkAnimation()         =>  Calls for walk aniamtion blend tree state;
 *  SetHoldingAnimation()       =>  Calls for holding animation blend tree state (This state is kept on a seperate override layer);
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
        public string HOLD = "Holding";
        public string DROP = "Default";
        public string BOUNCING = "Bouncing";
        public string NOT_BOUNCING = "NotBouncing";

        private void Start()
        {
            m_playerController = GetComponent<Player.Controller>();
            m_animator = GetComponent<Animator>();

            m_currentAnimationState = IDLE;
            m_animator.SetFloat("InputX", m_playerController.m_moveInput.x);
            m_animator.SetFloat("InputY", m_playerController.m_moveInput.y);
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

            // ----- holding items -----
            SetHoldingAnimation();

            // ----- rotation -----
            SetRotationFromInput();
        }

        private void PlayIdleAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, IDLE);
        }

        private void PlayWalkingAnimation()
        {
            m_currentAnimationState = AnimationController.ChangeAnimationState(m_animator, m_currentAnimationState, WALK);
        }

        private void SetHoldingAnimation()
        {
            if (m_playerController.m_currentlyHeldItem != null)
            {
                m_animator.Play(HOLD);
            }
            else
            {
                m_animator.Play(DROP);
            }
        }

        private void SetRotationFromInput()
        {
            if (m_playerController.m_moveInput != Vector2.zero)
            {
                m_animator.SetFloat("InputX", m_playerController.m_moveInput.x);
                m_animator.SetFloat("InputY", m_playerController.m_moveInput.y);
            } 
        }

        public void SetBouncingAnimation(bool isTrue)
        {
            if (isTrue) m_animator.Play(BOUNCING);
            if (!isTrue) m_animator.Play(NOT_BOUNCING);
        }
    }
}
