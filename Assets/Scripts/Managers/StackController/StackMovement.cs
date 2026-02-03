using UnityEngine;

public class StackMovement : MonoBehaviour
{
    private StackController m_stackController;
    private CharacterController m_characterController;

    public int m_movieInputIndex = 0;
    public Vector3 m_movementInput;

    [SerializeField] private float m_moveSpeed = 12.0f;
    [SerializeField] private float m_moveAcceleration = 5.0f;
    public Vector3 m_currentMoveVelocity;
    private Vector3 m_targetMoveVelocity;

    public StackMovement m_lowerController;

    private void Awake()
    {
        m_stackController = GetComponentInParent<StackController>();
        m_characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {

    }

    private void Update()
    {


        HandleMovement();
    }

    private void HandleMovement()
    {
        // ----- handle move input -----
        Vector2 input = m_stackController.m_movementInput[m_movieInputIndex];
        if (!m_stackController.m_canMove[m_movieInputIndex]) input = Vector2.zero;

        m_movementInput = new Vector3(input.x, 0, input.y);
        m_movementInput = Vector3.ClampMagnitude(m_movementInput, 1f);

        // ----- handle move velocity -----
        m_targetMoveVelocity = m_movementInput * m_moveSpeed;
        if (m_lowerController != null)
        {
            m_currentMoveVelocity += m_lowerController.m_currentMoveVelocity;
        }
        m_currentMoveVelocity = Vector3.Lerp(m_currentMoveVelocity, m_targetMoveVelocity, m_moveAcceleration * Time.deltaTime);

        // ----- apply move velocity -----
        m_characterController.Move(m_currentMoveVelocity * Time.deltaTime);
    }
}
