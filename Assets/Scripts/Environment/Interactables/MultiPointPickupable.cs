using Interactables;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Interactables
{
    public class MultiPointPickupable : Pickupable
    {
        public List<Pickupable> m_pickupPoints;
        private CharacterController m_characterController;

        public Vector3 m_itemMoveVelocity;
        public Vector3 m_targetMoveVelocity;

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            bool isAllPickupPointsGrabbed = true;
            float numberOfPickupPoints = m_pickupPoints.Count;
            float minValue = -1 * numberOfPickupPoints;
            float maxValue = 1 * numberOfPickupPoints;
            Vector2 combinedMovementForce = Vector2.zero;

            foreach (Pickupable pickupPoint in m_pickupPoints)
            {
                if (!pickupPoint.m_isPickedUp) { isAllPickupPointsGrabbed = false; break; }

                combinedMovementForce += pickupPoint.m_playerController.m_moveInput;
            }

            combinedMovementForce /= m_pickupPoints.Count;
            float movementForceX = MathExtensions.Remap(
                    combinedMovementForce.x,
                    minValue, maxValue,
                    -1.0f, 1.0f
                );

            float movementForceY = MathExtensions.Remap(
                combinedMovementForce.y,
                minValue, maxValue,
                -1.0f, 1.0f
            );

            float speedMultiplier = isAllPickupPointsGrabbed ? 1.0f : 0.0f;
            combinedMovementForce *= speedMultiplier;
            Vector3 itemMovement = new Vector3(combinedMovementForce.x, 0, combinedMovementForce.y);
            itemMovement = Vector3.ClampMagnitude(itemMovement, 1f);

            // ----- handle move velocity -----
            if (m_pickupPoints[0].m_playerController != null)
            {
                float acceleration = m_pickupPoints[0].m_playerController.m_movement.m_moveSpeed;
                m_targetMoveVelocity = itemMovement * m_pickupPoints[0].m_playerController.m_movement.m_moveSpeed;
                m_itemMoveVelocity = Vector3.Lerp(m_itemMoveVelocity, m_targetMoveVelocity, acceleration * Time.deltaTime);

                m_characterController.Move(m_itemMoveVelocity * Time.deltaTime);
            }
        }

        public override void Interact(Controller playerController)
        {
            base.Interact(playerController);
        }

        private void CalculateObjectMovement()
        {

        }

    }

}

