using Player;
using UnityEngine;

/*
 *  Interactable type that can only be picked up by m_stackPlayersRequired or more.
 *  Used for pickign up the mirror. Players must stack up and then pick up mirror before moving the mirror while balancing.
 *  
 *  [DEPRECIATED] No longer used in game, mirror puzzle functionality was changed after player testing found it was too
 *  complicated for players to figure out to stack, pick up the mirror, then balance the stack while holding the mirror.
 */

namespace Interactables
{
    public class StackPickupable : Pickupable
    {
        private int m_stackedPlayersRequired = 2;

        public override void Interact(Controller playerController)
        {
            // ----- must have X number of players in stack -----
            if (playerController.GetComponentInChildren<Stack.Controller>().m_playersInStack != m_stackedPlayersRequired) return;
            
            // ----- get top stack player for object positioning ----
            Stack.Controller stackController = playerController.GetComponentInChildren<Stack.Controller>();
            int topPosition = stackController.m_topPlayerPosition;
            m_playerController = playerController.GetComponentInChildren<Stack.Controller>().m_playerControllers[topPosition];

            // ----- member variable assignments -----
            m_isPickedUp = true;
            m_collider.enabled = false;
            m_playerController.m_currentlyHeldItem = gameObject;

            // ---- handle multipoint pickup restrictions -----
            if (m_isMultipointPickupPoint)
            {
                m_playerController.m_canMove = false;
                m_playerController.transform.parent = transform.parent;
            }
            else
            {
                PositionItem();
            }
        }

        private void PositionItem()
        {
            // ----- move item along with player hold positioner -----
            transform.position = m_playerController.m_heldItemsPosition.transform.position;
        }
    }
}
