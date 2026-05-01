using Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private List<Vector2> m_directions;

        void Awake()
        {
            m_playerController = GetComponent<Player.Controller>();

            m_directions = new List<Vector2>();
            m_directions.Add(new Vector2(0.0f, 0.0f));

            m_directions.Add(new Vector2(1.0f, 0.0f));
            m_directions.Add(new Vector2(-1.0f,0.0f));
            m_directions.Add(new Vector2(0.0f, 1.0f));
            m_directions.Add(new Vector2(0.0f, -1.0f));

            m_directions.Add(new Vector2(0.707107f, 0.707107f));
            m_directions.Add(new Vector2(-0.707107f, -0.707107f));
            m_directions.Add(new Vector2(-0.707107f, 0.707107f));
            m_directions.Add(new Vector2(0.707107f, -0.707107f));
        }

        public void Move(InputAction.CallbackContext context)
        {
            m_playerController.m_moveInput = context.ReadValue<Vector2>();

            Vector2 closestDirection = m_directions[0];
            float closestDistance = Vector2.Distance(m_playerController.m_moveInput, m_directions[0]);
            foreach(Vector2 dir in m_directions)
            {
                float dist = Vector2.Distance(m_playerController.m_moveInput, dir);
                if(dist < closestDistance)
                {
                    closestDistance = dist;
                    closestDirection = dir;
                }
            }

            m_playerController.m_moveInput = closestDirection;
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (m_playerController.m_currentlyHeldItem != null)
                {
                    m_playerController.DropItem();
                }
                else
                {
                    m_playerController.Interact(m_playerController);
                }
            }
        }

        public void Pause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (TimeManager.instance.isGamePaused)
                {
                    TimeManager.instance.isGamePaused = false;
                    UIManager.instance.CloseMenu(UIManager.MENU.Pause);
                }
                else
                {
                    TimeManager.instance.isGamePaused = true;
                    UIManager.instance.OpenMenu(UIManager.MENU.Pause);
                    UIManager.instance.OpenMenu(UIManager.MENU.PauseContent);
                }
                
            }
        }

        public void Back(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (!TimeManager.instance.isGamePaused) return;
                
                if (UIManager.instance.m_settingsMenu.activeInHierarchy)
                {
                    UIManager.instance.CloseMenu(UIManager.MENU.Settings);
                    UIManager.instance.OpenMenu(UIManager.MENU.Pause);
                    UIManager.instance.OpenMenu(UIManager.MENU.PauseContent);
                    EventSystem.current.SetSelectedGameObject(UIManager.instance.m_resumeButton);
                    UIManager.instance.m_tabButtons.SetActive(false);
                }
                if (UIManager.instance.m_audioMenu.activeInHierarchy)
                {
                    StartCoroutine(UIManager.instance.WaitThenCloseMenu(UIManager.MENU.Audio));
                    UIManager.instance.OpenMenu(UIManager.MENU.Settings);
                    EventSystem.current.SetSelectedGameObject(UIManager.instance.m_audioButton);
                }
            }
        }
    }
}