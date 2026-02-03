using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private Player.Controller m_playerController;
        private CharacterController m_characterController;

        [SerializeField] private float m_moveSpeed = 12.0f;
        [SerializeField] private float m_moveSpeedStandard = 12.0f;
        [SerializeField] private float m_moveSpeedStackedBottom = 4.0f;
        [SerializeField] private float m_moveSpeedStackedSecond = 4.0f;
        [SerializeField] private float m_moveSpeedStackedThird = 6.0f;
        [SerializeField] private float m_moveSpeedStackedTop = 8.0f;

        [SerializeField, Range(0f, 1f)] private float m_stackMoveModifier = 0.5f;
        [SerializeField] private float m_moveAcceleration = 5.0f;
        public Vector3 m_currentMoveVelocity;
        private Vector3 m_targetMoveVelocity;

        public float m_rotation;
        public Vector3 m_movementInput;

        private void Awake()
        {
            m_playerController = GetComponentInParent<Player.Controller>();
            m_characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {

        }

        private void Update()
        {


            HandleMovement();
            HandleFalseRotation();
        }

        private void HandleMovement()
        {
            // ----- handle move input -----
            Vector2 input = m_playerController.m_movementInput;
            if (!m_playerController.m_canMove) input = Vector2.zero;

            m_movementInput = new Vector3(input.x, 0, input.y);
            m_movementInput = Vector3.ClampMagnitude(m_movementInput, 1f);

            // ----- handle move velocity -----
            m_targetMoveVelocity = m_movementInput * m_moveSpeed;
            m_currentMoveVelocity = Vector3.Lerp(m_currentMoveVelocity, m_targetMoveVelocity, m_moveAcceleration * Time.deltaTime);

            // ----- apply move velocity -----
            m_characterController.Move(m_currentMoveVelocity * Time.deltaTime);
        }

        private void HandleFalseRotation()
        {
            // ----- handle rotation input -----
            Vector2 input = m_playerController.m_movementInput;
            if (!m_playerController.m_canMove) input = Vector2.zero;

            Vector3 movementInput = new Vector3(input.x, 0, input.y);
            movementInput = Vector3.ClampMagnitude(movementInput, 1f);

            // ----- handle rotation calculations -----
            if(input != Vector2.zero)
            {
                m_rotation = Mathf.Atan2(movementInput.x, movementInput.z);
                m_rotation *= Mathf.Rad2Deg;
            }

            m_playerController.m_rotation = m_rotation;
        }
    }
}
