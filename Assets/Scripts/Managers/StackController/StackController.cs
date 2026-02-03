using UnityEngine;
using System.Collections.Generic;

public class StackController : MonoBehaviour
{
    private InputManager m_inputManager;
    private Player.Movement m_movement;

    [Header("Statuses")]
    public Vector3 m_currentMovementInput;
    public bool[] m_canMove = new bool[4];

    public Vector2[] m_movementInput = new Vector2[4];

    private void Awake()
    {
        m_movement = GetComponentInChildren<Player.Movement>();
    }

    private void Start()
    {
        m_inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        m_movementInput[0] = m_inputManager.GetMovementInput();
        if (!m_canMove[0]) m_movementInput[0] = Vector2.zero;

        m_movementInput[1] = m_inputManager.GetMovementInput();
        if (!m_canMove[1]) m_movementInput[1] = Vector2.zero;

        m_movementInput[2] = m_inputManager.GetMovementInput();
        if (!m_canMove[2]) m_movementInput[2] = Vector2.zero;

        m_movementInput[3] = m_inputManager.GetMovementInput();
        if (!m_canMove[3]) m_movementInput[3] = Vector2.zero;
    }
}
