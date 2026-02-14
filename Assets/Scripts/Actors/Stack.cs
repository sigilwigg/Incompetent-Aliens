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

        private void Awake()
        {
        }

        private void Update()
        {
            CountPlayersInStack();

            if(m_playersInStack == 1)
            {
                m_playerControllers[0].m_movement.m_influencingStrength = 0;
            } else
            {
                m_playerControllers[0].m_movement.m_influencingStrength = 0.25f;
            }
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