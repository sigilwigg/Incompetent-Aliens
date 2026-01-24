using Player;
using UnityEngine;

namespace Interactables
{
    public class PickupInteractable : Interactable
    {
        private Controller playerController;

        [SerializeField]
        private string m_itemName;

        private void Start()
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<Controller>();
        }

        public override void Interact()
        {
            base.Interact();

            playerController.m_currentlyHeldItem = gameObject;
            
        }
    }

}
