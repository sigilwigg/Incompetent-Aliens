using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private InputManager m_inputManager;
        private Player.Controller m_playerController;
        private CharacterController m_characterController;

        public float m_gravityForce = 5.0f;
        public float m_moveSpeed = 12.0f;
        [SerializeField] private float m_moveAcceleration = 5.0f;
        private Vector3 m_currentMoveVelocity;
        private Vector3 m_targetMoveVelocity;

        public float m_rotation;
        private bool m_isGrounded;
        public float m_groundCheckDistance = 0.1f;
        public float m_groundCheckRadius = 0.25f;

        public Transform m_influencingTransform;
        public float m_influencingStrength = 0.5f;

        public LayerMask m_groundLayer;

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
            CheckIsGrounded();
            HandleMovement();
            HandleFalseRotation();
        }

        private void CheckIsGrounded()
        {
            if (m_playerController.m_isStacked)
            {
                m_isGrounded = true;
                return;
            }

            RaycastHit hit;

            Vector3 groudCheckPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (Physics.SphereCast(groudCheckPosition, m_groundCheckRadius, Vector3.down, out hit, m_groundCheckDistance, m_groundLayer))
            {
                m_isGrounded = true;
            } else
            {
                m_isGrounded = false;
            }

            if (Physics.SphereCast(groudCheckPosition, m_groundCheckRadius, Vector3.down, out hit, m_groundCheckDistance, m_groundLayer))
            {
                m_isGrounded = true;
            }
            else
            {
                m_isGrounded = false;
            }
        }

        private void HandleMovement()
        {
            if (!m_playerController.m_canMove) return;

            // ----- handle move input -----
            Vector2 input = m_playerController.m_moveInput;

            
            Vector3 movementInput = new Vector3(input.x, 0, input.y);
            movementInput = Vector3.ClampMagnitude(movementInput, 1f);

            // ----- handle influencing transform -----
            if (m_influencingTransform != null && m_influencingStrength > 0.0f)
            {
                Vector3 influencingDistanceFromCenter = new Vector3(m_influencingTransform.localPosition.x, 0, m_influencingTransform.localPosition.z);
                influencingDistanceFromCenter *= m_influencingStrength;
                movementInput += influencingDistanceFromCenter;
                movementInput /= 2.0f;
            }

            // ----- handle move velocity -----
            m_targetMoveVelocity = movementInput * m_moveSpeed;
            if (!m_isGrounded && m_playerController.m_stackPosition == 0) 
                m_targetMoveVelocity.y = -m_gravityForce;
            m_currentMoveVelocity = Vector3.Lerp(m_currentMoveVelocity, m_targetMoveVelocity, m_moveAcceleration * Time.deltaTime);
            m_characterController.Move(m_currentMoveVelocity * Time.deltaTime);

            if (movementInput == Vector3.zero)
            {
                m_playerController.m_moveState = Controller.MoveState.Idle;
            }
            else
            {
                m_playerController.m_moveState = Controller.MoveState.Walking;
            }
        }

        private void HandleFalseRotation()
        {
            // ----- handle rotation input -----
            Vector2 input = m_playerController.m_moveInput;
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

        private void OnDrawGizmos()
        {
            // Set the color with custom alpha.
            Gizmos.color = new Color(1f, 0f, 0f, 0.25f); // Red with custom alpha
            if (m_isGrounded)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            }

            // Draw the sphere.
            Vector3 groudCheckPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            Gizmos.DrawSphere(groudCheckPosition, m_groundCheckRadius);
        }
    }
}
