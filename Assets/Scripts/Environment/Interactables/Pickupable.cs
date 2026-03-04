using Player;
using UnityEngine;

/*
 * Interactables that can be picked up by the player.
 * 
 * Interact()       => standard interact override method, handles what to do for interact.
 * PositionItem()   => positions item in player hold position initially.
 */

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

            if (!m_isMultipointPickupPoint)
            {
                PositionItem();
            }
            else
            {
                playerController.m_canMove = false;
                playerController.transform.parent = transform.parent;
            }

        }

        private void PositionItem()
        {
            transform.position = m_playerController.m_heldItemsPosition.transform.position;
        }
    }

}
