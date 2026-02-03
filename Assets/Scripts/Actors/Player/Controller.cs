using Interactables;
using UnityEngine;

namespace Player
{
    public enum StackPosition
    {
        BOTTOM,
        SECOND,
        THIRD,
        TOP
    }

    public class Controller : MonoBehaviour
    {
        private InputManager m_inputManager;
        private Player.Movement m_movement;

        [Header("Core")]
        public Transform m_rotationTransform;

        [Header("Statuses")]
        public Vector3 m_currentMovementInput;
        public bool m_canMove;
        public float m_rotation;
        public bool m_isStacked;

        [Header("Interact")]
        public bool m_isInteractableObjectAvailable = false;
        public Interactables.Interactable m_availableInteractableObject;
        public GameObject m_currentlyHeldItem;
        public Transform m_heldItemsPosition;
        public Transform m_interactableCheckOrigin;

        [Header("Interact")]
        public bool m_isStackablePlayerAvailable = false;
        public Player.Stack m_availableStackablePlayer;

        public Vector2 m_movementInput = Vector2.zero;

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
            //m_rotationTransform.rotation = Quaternion.Euler(0, 180 + m_rotation, 0);
        }

        private void UpdateInput()
        {
            m_movementInput = m_inputManager.GetMovementInput();
            if (!m_canMove) m_movementInput = Vector2.zero;
        }

        // ========== STACKING ==========
        public void Stack()
        {
            if (m_isStackablePlayerAvailable)
            {
                Debug.Log("stack");
                m_availableStackablePlayer.StackUp(
                    this,
                    m_movement.gameObject.transform
                );
            }
        }

        // ========== INTERACTABLES ==========
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