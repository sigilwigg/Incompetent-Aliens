using Interactables;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Player.InputManager m_inputManager;
        public enum MoveState
        {
            Idle,
            Walking
        }

        public Player.Movement m_movement;
        private Player.Animations m_animations;

        //----- model ref -----
        public GameObject m_playerModel;

        [Header("Core")]
        public Transform m_rotationTransform;
        public GameObject m_particleTrailVFX;

        [Header("Stacking")]
        public MatchPosition m_playerMatchPosition;
        public Stack.Controller m_stackController;
        public Stack.Controller m_myStackController;
        public int m_stackPosition;

        [Header("Statuses")]
        public bool m_isStacked;
        public Vector2 m_moveInput;
        public Vector2 m_influencedMoveInput;
        public bool m_canMove;
        public float m_rotation;
        public MoveState m_moveState;

        [Header("Interact")]
        public bool m_isInteractableObjectAvailable = false;
        public Interactables.Interactable m_availableInteractableObject;
        public GameObject m_currentlyHeldItem;
        public Transform m_heldItemsPosition;
        public Transform m_interactableCheckOrigin;

        [Header("SpriteReferences")]
        public SpriteRenderer m_bodySprite;
        public SpriteRenderer m_handLeftSprite;
        public SpriteRenderer m_handRightSprite;
        public SpriteRenderer m_headSprite;

        private void Awake()
        {
            m_movement = GetComponentInChildren<Player.Movement>();
            m_animations = GetComponent<Player.Animations>();
        }

        private void Start()
        {
            m_inputManager = GetComponent<Player.InputManager>();

            m_playerMatchPosition.enabled = false;
        }

        private void Update()
        {
            m_rotationTransform.rotation = Quaternion.Euler(0, 180 + m_rotation, 0);
            UpdateItemPosition();
        }

        public void SetColor(Color color)
        {
            m_bodySprite.color = color;
            m_handLeftSprite.color = color;
            m_handRightSprite.color = color;
            m_headSprite.color = color;
        }

        // =========== INTERACTABLES ===========
        public void Interact(Player.Controller playerController)
        {
            if (m_isInteractableObjectAvailable)
            {
                m_availableInteractableObject.Interact(playerController);
            }
            else if (m_isStacked && m_stackController != null)
            {
                m_stackController.RemoveFromStack(m_stackPosition);
            }
        }

        public void DropItem() 
        {
            if (m_currentlyHeldItem == null) return;
            Pickupable itemToDrop = m_currentlyHeldItem.GetComponent<Interactables.Pickupable>();

            itemToDrop.m_isPickedUp = false;

            if (itemToDrop != null) itemToDrop.m_collider.enabled = true;
            if(itemToDrop.m_isMultipointPickupPoint) m_canMove = true;
            m_currentlyHeldItem = null;
        }

        public void UpdateItemPosition()
        {
            if (m_currentlyHeldItem == null) return;
            m_currentlyHeldItem.transform.position = m_heldItemsPosition.position;
        }

        public void SetBeingThrown(Transform bouncyTransform)
        {
            DropItem();
            GetComponentInChildren<Player.Movement>().enabled = false;
            GetComponentInChildren<CharacterController>().enabled = false;
            m_particleTrailVFX.SetActive(false);

            m_playerMatchPosition.enabled = true;
            m_playerMatchPosition.m_targetTransform = bouncyTransform;

            m_animations.SetBouncingAnimation(true);
            Invoke(nameof(UnSetBeingThrown), 1.5f);
        }

        private void UnSetBeingThrown()
        {
            GetComponentInChildren<Player.Movement>().enabled = true;
            GetComponentInChildren<CharacterController>().enabled = true;
            m_particleTrailVFX.SetActive(true);

            m_playerMatchPosition.enabled = false;
            m_playerMatchPosition.m_targetTransform = null;

            m_animations.SetBouncingAnimation(false);
        }
    }
}