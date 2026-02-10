using Player;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 *  Streamlines input control through one class that either passes information onto the PlayerController
 *  Or is called by the PlayerController to access streamed input values.
 * 
 *      public GetMovementInput()           => vector2 of wasd input between 0 and 1. Called from other scripts as needed.
 *      void InteractPerformed()            => calls appropriate method in player controller.
 */

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        private Player.Controller m_playerController;

        public Vector2 m_moveInput;

        void Awake()
        {
            m_playerController = GetComponent<Controller>();
        }

        public void Move(InputAction.CallbackContext context)
        {
            m_moveInput = context.ReadValue<Vector2>();
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (m_playerController.m_currentlyHeldItem != null)
            {
                m_playerController.DropItem();
            }
            else
            {
                m_playerController.Interact();
            }
        }
    }
}