using Player;
using UnityEngine;

namespace Interactables
{
    public class PickupInteractable : Interactable
    {
        private Controller m_playerController;
        private HeldItemTransform m_heldItemTransform;

        [SerializeField]
        private string m_itemName;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            m_playerController = player.GetComponent<Controller>();
            m_heldItemTransform = player.GetComponentInChildren<HeldItemTransform>();
        }

        public override void Interact()
        {
            base.Interact();

            m_playerController.m_currentlyHeldItem = gameObject;
            PositionItem();
            
        }

        
        private void PositionItem()
        {
            transform.parent = m_heldItemTransform.transform;
            transform.position = m_heldItemTransform.transform.position;
        }
    }

}
