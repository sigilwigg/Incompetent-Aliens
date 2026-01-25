using Interactables;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Player.Movement m_movement;

        public bool m_canMove;

        public bool m_isInteractableObjectAvailable = false;
        public Interactables.Interactable m_availableInteractableObject;
        public GameObject m_currentlyHeldItem;

        private void Awake()
        {
            m_movement = GetComponentInChildren<Player.Movement>();
        }
         
        // ----- INTERACTABLES -----
        public void Interact()
        {
            if (m_isInteractableObjectAvailable)
            {
                Debug.Log("Successfully Interacted!");
                m_availableInteractableObject.Interact();
            }
        }

        //Called when the player inputs the Interact key when holding an item
        public void DropItem() 
        {
            PickupInteractable itemToDrop = m_currentlyHeldItem.GetComponent<Interactables.PickupInteractable>();

            itemToDrop.gameObject.transform.parent = null;

            m_currentlyHeldItem = null;

        }
    }
}