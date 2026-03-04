using Player;
using UnityEngine;

namespace Interactables
{
    public class StackPickupable : Pickupable
    {
        private int m_stackedPlayersRequired = 2;

        public override void Interact(Controller playerController)
        {
            if (playerController.GetComponentInChildren<Stack.Controller>().m_playersInStack != m_stackedPlayersRequired) return;
            
            Stack.Controller stackController = playerController.GetComponentInChildren<Stack.Controller>();
            int topPosition = stackController.m_topPlayerPosition;
            m_playerController = playerController.GetComponentInChildren<Stack.Controller>().m_playerControllers[topPosition];

            m_isPickedUp = true;
            m_collider.enabled = false;
            m_playerController.m_currentlyHeldItem = gameObject;

            if (!m_isMultipointPickupPoint)
            {
                PositionItem();
            }
            else
            {
                m_playerController.m_canMove = false;
                m_playerController.transform.parent = transform.parent;
            }
        }

        private void PositionItem()
        {
            transform.position = m_playerController.m_heldItemsPosition.transform.position;
        }
    }
}
