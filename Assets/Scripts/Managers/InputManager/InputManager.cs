using NUnit.Framework.Interfaces;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 *  Streamlines input control through one class that either passes information onto the PlayerController
 *  Or is called by the PlayerController to access streamed input values.
 * 
 *      public GetMovementInput()           => vector2 of wasd input between 0 and 1. Called from other scripts as needed.
 *      void InteractPerformed()            => calls appropriate method in player controller.
 */

public class InputManager : MonoBehaviour
{
    private InputMaster m_input;
    public Controller m_playerController;

    void Awake()
    {
        m_input = new InputMaster();
        m_input.Player.Interact.performed += InteractPerformed;
        m_input.Player.Stack.performed += StackPerformed;
    }

    private void Start()
    {
        m_playerController = GameObject.FindWithTag("Player").GetComponent<Controller>();
    }

    public Vector2 GetMovementInput()
    {
        return m_input.Player.Move.ReadValue<Vector2>();
    }

    void InteractPerformed(InputAction.CallbackContext context)
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

    void StackPerformed(InputAction.CallbackContext context)
    {
        if (m_playerController.m_isStacked == true)
        {
            // TODO:
        }
        else
        {
            Debug.Log("stacking");
            m_playerController.Stack();
        }

    }

    void OnEnable()
    {
        m_input.Enable();
    }

    void OnDisable()
    {
        m_input.Disable();
    }
}
