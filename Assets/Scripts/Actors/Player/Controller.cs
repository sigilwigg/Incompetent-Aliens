using Interactables;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private InputManager m_inputManager;
        public enum MoveState
        {
            Idle,
            Walking
        }

        private Player.Movement m_movement;

        [Header("Core")]
        public Transform m_rotationTransform;

        [Header("Statuses")]
        public Vector2 m_input;
        public bool m_canMove;
        public float m_rotation;
        public MoveState m_moveState;

        [Header("Interact")]
        public bool m_isInteractableObjectAvailable = false;
        public Interactables.Interactable m_availableInteractableObject;
        public GameObject m_currentlyHeldItem;
        public Transform m_heldItemsPosition;
        public Transform m_interactableCheckOrigin;
        

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
            GetInput();
            m_rotationTransform.rotation = Quaternion.Euler(0, 180 + m_rotation, 0);
        }

        private void GetInput()
        {
            m_input = m_inputManager.GetMovementInput();
        }

        // =========== INTERACTABLES ===========
        public void Interact()
        {
            if (m_isInteractableObjectAvailable)
            {
                m_availableInteractableObject.Interact();
            }
        }

        // Called when the player inputs the Interact key when holding an item
        public void DropItem() 
        {
            Pickupable itemToDrop = m_currentlyHeldItem.GetComponent<Interactables.Pickupable>();

            if (itemToDrop != null) itemToDrop.m_collider.enabled = true;
            itemToDrop.gameObject.transform.parent = null;
            m_currentlyHeldItem = null;
        }
    }
}