using Player;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;


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
        private int m_interactRange;

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
            m_playerController.m_isInteractableObjectAvailable = false;
            m_playerController.m_availableInteractableObject = null;

            Collider nearestInteractable = null;
            float nearestDistance = float.MaxValue;
            float distance;


            Collider[] interactableColliders = Physics.OverlapSphere(gameObject.transform.position, m_interactRange, m_interactableLayerMask);
            foreach (var interactable in interactableColliders)
            {
                distance = Vector3.Distance(gameObject.transform.position, interactable.gameObject.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactable;
                }
            }

            if (nearestInteractable != null)
            {
                m_playerController.m_availableInteractableObject = nearestInteractable.gameObject.GetComponent<Interactable>();
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
