using UnityEngine;
using System.Collections.Generic;

namespace Player
{
    public class Stack : MonoBehaviour
    {
        public int m_stackHeight = 1;
        public Player.Controller m_playerController;
        public Player.Controller[] m_playerControllers = new Player.Controller[4];
        public StackPhysics m_stackPhysics;

        private void Start()
        {
            m_playerControllers[0] = m_playerController;
            m_stackPhysics = GetComponent<StackPhysics>();
        }

        public void StackUp(Player.Controller playerController, Transform playerBody)
        {
            Debug.Log("stackup");
            playerController.m_isStacked = true;
            m_playerController.m_isStacked = true;

            m_stackHeight++;
            m_playerControllers[m_stackHeight - 1] = playerController;

            playerBody.gameObject.GetComponent<CharacterController>().enabled = false;
        }
    }
}
