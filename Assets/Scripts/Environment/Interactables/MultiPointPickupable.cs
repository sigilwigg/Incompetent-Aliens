using Interactables;
using System.Collections.Generic;
using Player;
using UnityEngine;
using JetBrains.Annotations;

/*
 * Subclass inherited from the "pickupable" script. Placed on objects which require multiple players to pick up an object from multiple points in order for it to move. 
 * 
 * CalculateObjectMovement()    =>  Calculates the movement of the interactable object using the input values of all players currently holding the object. If not enough players are holding the object, it will not move
 * 
 * 
*/

namespace Interactables
{
    public class MultiPointPickupable : Pickupable
    {
        public List<Pickupable> m_pickupPoints;
        private CharacterController m_characterController;

        public bool isAllPickupPointsGrabbed;

        public Vector3 m_itemMoveVelocity;
        public Vector3 m_targetMoveVelocity;
        private Vector2 combinedMovementForce = Vector2.zero;

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isAllPickupPointsGrabbed = true;

            // ----- Gathers the movement input values of each player holding the object, and add them together into a combined movement force -----
            foreach (Pickupable pickupPoint in m_pickupPoints) 
            {
                if (!pickupPoint.m_isPickedUp) { isAllPickupPointsGrabbed = false; break; }

                combinedMovementForce += pickupPoint.m_playerController.m_moveInput;
            }

            CalculateObjectMovement();

        }

        public override void Interact(Controller playerController)
        {
            base.Interact(playerController);
        }

        private void CalculateObjectMovement()
        {
            if (m_pickupPoints[0].m_playerController == null) return;

            float numberOfPickupPoints = m_pickupPoints.Count;
            float minValue = -1 * numberOfPickupPoints;
            float maxValue = 1 * numberOfPickupPoints;


            combinedMovementForce /= m_pickupPoints.Count;

            // ----- Ensure the combined movement force remains at a value between -1 and 1 so as to not exceed players' max speed -----
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

            // ----- Stop object movement if number of required players is not reached -----
            float speedMultiplier = isAllPickupPointsGrabbed ? 1.0f : 0.0f;
            combinedMovementForce *= speedMultiplier;
            Vector3 itemMovement = new Vector3(combinedMovementForce.x, 0, combinedMovementForce.y);
            itemMovement = Vector3.ClampMagnitude(itemMovement, 1f);

            // ----- handle move velocity -----
            float acceleration = m_pickupPoints[0].m_playerController.m_movement.m_moveSpeed;
            m_targetMoveVelocity = itemMovement * m_pickupPoints[0].m_playerController.m_movement.m_moveSpeed;
            m_itemMoveVelocity = Vector3.Lerp(m_itemMoveVelocity, m_targetMoveVelocity, acceleration * Time.deltaTime);

            m_characterController.Move(m_itemMoveVelocity * Time.deltaTime);

        }

    }

}

