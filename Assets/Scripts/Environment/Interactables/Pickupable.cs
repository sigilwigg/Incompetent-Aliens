using Player;
using UnityEngine;

namespace Interactables
{
    public class Pickupable : Interactable
    {
        private Controller m_playerController;
        public Collider m_collider;

        [SerializeField]
        private string m_itemName;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            m_playerController = player.GetComponent<Controller>();
            m_collider = GetComponent<Collider>();
        }

        public override void Interact()
        {
            base.Interact();

            m_playerController.m_currentlyHeldItem = gameObject;
            m_collider.enabled = false;
            PositionItem();
        }

        private void PositionItem()
        {
            transform.parent = m_playerController.m_heldItemsPosition.transform;
            transform.position = m_playerController.m_heldItemsPosition.transform.position;
        }
    }

}
