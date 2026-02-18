using System.Collections.Generic;
using UnityEngine;

namespace Stack
{
    public class Controller : MonoBehaviour
    {
        public List<Transform> m_stackPositionTransforms = new List<Transform>();
        public List<Player.Controller> m_playerControllers = new List<Player.Controller>();

        public int m_playersInStack = 0;
        public int m_topPlayerPosition;

        [Range(0.0f, 1.0f)]
        public float m_baseInfluencingStrength = 0.5f;

        private void Awake()
        {
        }

        private void Update()
        {
            CountPlayersInStack();

            if(m_playersInStack == 1)
            {
                m_playerControllers[0].m_isStacked = false;
                m_playerControllers[0].m_movement.m_influencingStrength = 0;
            } 
            else if (m_playersInStack > 1)
            {
                m_playerControllers[0].m_movement.m_influencingStrength = m_baseInfluencingStrength;
            }
        }

        public void AddToStack(Player.Controller playerController)
        {
            if (m_playersInStack == 4) return;

            m_playerControllers[m_playersInStack] = playerController;
            playerController.GetComponentInChildren<Player.Movement>().enabled = false;
            playerController.GetComponentInChildren<CharacterController>().enabled = false;

            playerController.m_playerMatchPosition.enabled = true;
            playerController.m_playerMatchPosition.m_targetTransform = m_stackPositionTransforms[m_playersInStack];

            playerController.m_isStacked = true;
            playerController.m_stackController = this;
            playerController.m_stackPosition = m_playersInStack;
        }

        public void RemoveFromStack(int stackPosition)
        {
            // ----- can only unstack if is top player in stack -----
            if(m_playersInStack == stackPosition + 1) return;

            Player.Controller playerController = m_playerControllers[stackPosition];
            playerController.GetComponentInChildren<Player.Movement>().enabled = true;
            playerController.GetComponentInChildren<CharacterController>().enabled = true;

            playerController.m_playerMatchPosition.m_targetTransform = null;
            playerController.m_playerMatchPosition.enabled = false;

            playerController.m_isStacked = false;
            playerController.m_stackController = null;
            playerController.m_stackPosition = -1;
        }

        private void CountPlayersInStack()
        {
            if (m_playerControllers.Count == 0)
            {
                m_playersInStack = 0;
                return;
            }

            m_playersInStack = 0;
            foreach (Player.Controller playerController in m_playerControllers)
            {
                if (playerController != null) m_playersInStack++;
            }

            m_topPlayerPosition = m_playersInStack - 1;
        }
    }
}