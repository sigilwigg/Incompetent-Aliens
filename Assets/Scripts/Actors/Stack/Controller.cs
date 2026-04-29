using System.Collections.Generic;
using UnityEngine;

/*
 * Stack controller for managing players in a stack.
 * 
 * CountPlayersInStack()    =>  Counts players attatched to this stack.
 * AddToStack()             => publically callable method a player can call to atach itself to the stack if room is avialable.
 * RemoveFromStack()        => publically callable method a player can use to remove itself from the stack.
 */

namespace Stack
{
    public class Controller : MonoBehaviour
    {
        public Player.Controller m_playerController;
        public StackLean m_stackLean;
        public List<Transform> m_stackPositionTransforms = new List<Transform>();
        public List<Player.Controller> m_playerControllers = new List<Player.Controller>();

        public int m_playersInStack = 1;
        public int m_topPlayerPosition;

        [Range(-2.0f, 5.0f)]
        public float m_baseInfluencingStrength = 4.0f;

        private void Start()
        {
            m_playerControllers[0] = m_playerController;
            m_playerControllers[0].m_movement.m_influencingTransform = m_stackPositionTransforms[0]; 
        }

        private void Update()
        {
            CountPlayersInStack();

            if(m_playersInStack == 1)
            {
                m_playerControllers[0].m_movement.m_influencingStrength = 0;

                if(m_playerController.m_stackController == null) m_playerControllers[0].m_isStacked = false;
            } 
            else if (m_playersInStack > 1)
            {
                m_playerControllers[0].m_isStacked = true;
                m_playerControllers[0].m_movement.m_influencingStrength = m_baseInfluencingStrength;
            }

            if(m_playersInStack > 1 && m_stackLean.m_isCounteracted)
            {
                m_playerControllers[0].m_movement.m_influencingStrength = 0.0f;
            }
        }

        public void AddToStack(Player.Controller playerController)
        {
            // ----- can only run if stack isn't full -----
            if (m_playersInStack == 4) return;

            // ----- get controller, disable movement -----
            m_playerControllers[m_playersInStack] = playerController;
            playerController.m_canMove = false;
            playerController.GetComponentInChildren<CharacterController>().enabled = false;
            playerController.m_particleTrailVFX.SetActive(false);

            // ----- snap player to stack position -----
            playerController.m_playerMatchPosition.enabled = true;
            playerController.m_playerMatchPosition.m_targetTransform = m_stackPositionTransforms[m_playersInStack];

            // ----- update statuses -----
            playerController.m_isStacked = true;
            playerController.m_stackController = this;
            playerController.m_stackPosition = m_playersInStack;

            // ----- stack leaning adjustments -----
            if(m_playersInStack == 1) m_stackLean.RestartLean();
        }

        public void RemoveFromStack(int stackPosition)
        {
            // ----- can only unstack if is top player in stack -----
            if(m_playersInStack != stackPosition + 1) return;
            Debug.Log("remove :)");
            // ----- get controller, enable movement -----
            Player.Controller playerController = m_playerControllers[stackPosition];
            playerController.m_canMove = true;
            playerController.GetComponentInChildren<CharacterController>().enabled = true;
            playerController.m_particleTrailVFX.SetActive(true);

            // ----- un-snap player to stack position -----
            playerController.m_playerMatchPosition.m_targetTransform = null;
            playerController.m_playerMatchPosition.enabled = false;

            // ----- update statuses -----
            playerController.m_isStacked = false;
            playerController.m_stackController = null;
            playerController.m_stackPosition = 0;

            m_playerControllers[stackPosition] = null;

            CountPlayersInStack();
            if (m_playersInStack == 1) m_playerControllers[0].m_isStacked = false;
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