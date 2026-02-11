using Player;
using UnityEngine;

namespace Interactables
{
    public class Pickupable : Interactable
    {
        public Controller m_playerController;
        public Collider m_collider;
        public bool m_isPickedUp = false;
        public bool m_isMultipointPickupPoint = false;

        [SerializeField]
        private string m_itemName;

        private void Start()
        {
            m_collider = GetComponent<Collider>();
        }

        public override void Interact(Player.Controller playerController)
        {
            base.Interact(playerController);
            m_playerController = playerController;

            m_isPickedUp = true; 

            m_playerController.m_currentlyHeldItem = gameObject;
            m_collider.enabled = false;

            if(!m_isMultipointPickupPoint)
                PositionItem();
        }

        private void PositionItem()
        {
            //transform.parent = m_playerController.m_heldItemsPosition.transform;
            transform.position = m_playerController.m_heldItemsPosition.transform.position;
        }
    }

}
