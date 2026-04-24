using UnityEngine;

/*
 * Simple interactable for stacks specifically. Calls necessary methods on stack controller.
 */

namespace Interactables
{
    public class StackOfPlayers : Interactable
    {
        Stack.Controller m_stackController;
        Player.Controller m_playerController;

        private void Start()
        {
            m_stackController = GetComponent<Stack.Controller>();
        }

        public override void Interact(Player.Controller playerController)
        {
            if (!m_playerController.m_isStacked)
            {
                m_stackController.AddToStack(playerController);
            }
            
        }
    }
}
