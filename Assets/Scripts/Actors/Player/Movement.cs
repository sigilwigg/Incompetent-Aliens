using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private InputManager m_inputManager;
        private Player.Controller m_playerController;
        private CharacterController m_characterController;

        [SerializeField] private float m_moveSpeed = 12.0f;
        [SerializeField] private float m_moveAcceleration = 5.0f;
        private Vector3 m_currentMoveVelocity;
        private Vector3 m_targetMoveVelocity;

        private void Awake()
        {
            m_playerController = GetComponentInParent<Player.Controller>();
            m_characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            m_inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            //if (!m_playerController.m_canMove) return;

            // ----- handle move input -----
            Vector2 input = m_inputManager.GetMovementInput();
            if (!m_playerController.m_canMove) input = Vector2.zero;

            Vector3 movementInput = new Vector3(input.x, 0, input.y);
            movementInput = Vector3.ClampMagnitude(movementInput, 1f);

            // ----- handle move velocity -----
            m_targetMoveVelocity = movementInput * m_moveSpeed;
            m_currentMoveVelocity = Vector3.Lerp(m_currentMoveVelocity, m_targetMoveVelocity, m_moveAcceleration * Time.deltaTime);
            m_characterController.Move(m_currentMoveVelocity * Time.deltaTime);
        }
    }
}
