using UnityEngine;

namespace Interactables
{
    public class StackOfPlayers : Interactable
    {
        Stack.Controller m_stackController;

        private void Start()
        {
            m_stackController = GetComponent<Stack.Controller>();
        }

        public override void Interact(Player.Controller playerController)
        {
            m_stackController.AddToStack(playerController);
        }
    }
}
