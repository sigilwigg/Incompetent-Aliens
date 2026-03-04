using Player;
using UnityEngine;

namespace Interactables
{
    public class StackPickupable : Pickupable
    {
        private int m_stackedPlayersRequired;

        public override void Interact(Controller playerController)
        {
            if (playerController.m_stackController.m_playersInStack != m_stackedPlayersRequired) return;

            base.Interact(playerController);
        }
    }
}
