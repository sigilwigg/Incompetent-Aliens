using Interactables;
using TMPro;
using UnityEngine;


/*
 * Controls the Interact function of the player. When the player is in range of an interactable object, a prompt will appear and they will be able to press SPACE to interact with the object 
 * 
 * CheckForInteractable()           =>  Uses an overlap sphere to gather all the potential interactable objects in range of the player,
 *                                  before choosing the closest one and giving the player the option to interact with it
 * IsOwnCollider()                  =>  returns bool for if detected collider is our own
 * IsStackColliderInCurrentStack()  =>  retruns bool for if detected collider is part of the stack we're stacked in
 * 
*/
namespace Player
{
    public class Interact : MonoBehaviour
{
        private Controller m_playerController;
        private LayerMask m_interactableLayerMask;

        [SerializeField]
        private Canvas m_interactPrompt;
        [SerializeField]
        private TextMeshProUGUI m_promptText;

        [SerializeField]
        private float m_interactRange;

        private void Awake()
        {
            m_playerController = GetComponentInParent<Controller>();
            m_interactableLayerMask = LayerMask.GetMask("Interactable");
        }

        private void Update()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {

            // ----- Reset interactable available bool and available interactable object -----
            m_playerController.m_isInteractableObjectAvailable = false;
            m_playerController.m_availableInteractableObject = null;

            // ----- Used to determine the closest interactable Game Object -----
            Collider nearestInteractable = null;
            float nearestDistance = float.MaxValue;
            float distance;

            //----- Overlap Sphere Check -----
            Collider[] interactableColliders = Physics.OverlapSphere(
                m_playerController.m_interactableCheckOrigin.position,
                m_interactRange,
                m_interactableLayerMask
            );

            foreach (Collider interactable in interactableColliders)
            {
                // ----- prevent edge case detections -----
                if (IsOwnCollider(interactable)) continue;
                if (IsStackColliderInCurrentStack(interactable)) continue;
                if (
                    interactable.gameObject.GetComponent<Stack.Controller>() != null
                    && interactable.gameObject.GetComponent<Stack.Controller>().m_playerController.m_stackPosition != 0
                ) return;

                // ----- continue filtering by nearest distance -----
                // Get the distance between each interactable and the player
                distance = Vector3.Distance(gameObject.transform.position, interactable.gameObject.transform.position);

                // Determines the closest interactable to the player
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactable;
                }
            }

            // ----- nearest interactable set to available interactable -----
            if (nearestInteractable != null && m_playerController.m_currentlyHeldItem == null)
            {
                m_playerController.m_availableInteractableObject = nearestInteractable.gameObject.GetComponent<Interactables.Interactable>();
                m_playerController.m_isInteractableObjectAvailable = true;
                m_interactPrompt.enabled = true;
                m_promptText.text = m_playerController.m_availableInteractableObject.m_interactableText;
            }
            else
            {
                m_interactPrompt.enabled = false;
            }
        }

        private bool IsOwnCollider(Collider interactable)
        {
            // ----- make sure its not a collider on our own self -----
            if (interactable.gameObject.transform.parent == transform) return true;
            return false;
        }

        private bool IsStackColliderInCurrentStack(Collider interactable)
        {
            // ----- only check if player is stacked -----
            if (!m_playerController.m_isStacked) return false;

            // ----- ignore if is type pickupable -----
            bool isPickupableType = interactable.gameObject.GetComponent<Pickupable>() != null ? true : false;
            if (isPickupableType) return false;

            // ----- return if not has stack controller -----
            if (m_playerController.m_stackController == null) return true;

            // ----- if any of the stacks in our own stack match the interactable -----
            Stack.Controller myStackController = m_playerController.GetComponentInChildren<Stack.Controller>();
            foreach (Player.Controller playerController in myStackController.m_playerControllers)
            {
                if (playerController == null) continue;
                if (interactable.gameObject == playerController.m_stackController.gameObject) return true;
            }

            // ----- if any of the stacks in our stacked stack match the interactable -----
            Stack.Controller theStackImInStackController = m_playerController.m_stackController;
            if (theStackImInStackController.gameObject == interactable.gameObject) return true;

            foreach (Player.Controller playerController in theStackImInStackController.m_playerControllers)
            {
                if (playerController == null) continue;
                if (playerController.m_stackController != null && interactable.gameObject == playerController.m_stackController.gameObject) return true;
            }

            return false;
        }
    }
}
