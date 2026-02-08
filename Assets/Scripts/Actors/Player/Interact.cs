using Interactables;
using Player;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;


/*
 * Controls the Interact function of the player. When the player is in range of an interactable object, a prompt will appear and they will be able to press SPACE to interact with the object 
 * 
 * CheckForInteractable()       =>  Uses an overlap sphere to gather all the potential interactable objects in range of the player,
 *                                  before choosing the closest one and giving the player the option to interact with it
 * 
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

            foreach (var interactable in interactableColliders)
            {
                //Get the distance between each interactable and the player
                distance = Vector3.Distance(gameObject.transform.position, interactable.gameObject.transform.position);

                //Determines the closest interactable to the player
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactable;
                }
            }

            // Set the nearest Interactable to the current available interactable 
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
           
    }

}
