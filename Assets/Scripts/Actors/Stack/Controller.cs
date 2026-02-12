using System.Collections.Generic;
using UnityEngine;

namespace Stack
{
    public class StackController : MonoBehaviour
    {
        private Animator m_animator;
        public List<Player.Controller> m_playerControllers = new List<Player.Controller>();
        public List<Vector2> m_playerInputs = new List<Vector2>();
        public List<float> m_modifiers = new List<float>();

        public int m_playersInStack = 0;
        public int m_topPlayerPosition;

        private void Awake()
        {
            // ----- get necessary components -----
            m_animator = GetComponent<Animator>();

            // ----- set all 4 possible inputs to 0 -----
            m_playerInputs[0] = Vector2.zero;
            m_playerInputs[1] = Vector2.zero;
            m_playerInputs[2] = Vector2.zero;
            m_playerInputs[3] = Vector2.zero;
        }

        private void Update()
        {
            CountPlayersInStack();
            SetInputsForEachPlayer();
            ModifyInputsForEachPlayer();
            SetAnimationParameters();
        }

        private void SetAnimationParameters()
        {
            m_animator.SetFloat("MidLowX", m_playerInputs[1].x);
            m_animator.SetFloat("MidLowY", m_playerInputs[1].y);

            m_animator.SetFloat("MidTopX", m_playerInputs[2].x);
            m_animator.SetFloat("MidTopY", m_playerInputs[2].y);

            m_animator.SetFloat("TopX", m_playerInputs[3].x);
            m_animator.SetFloat("TopY", m_playerInputs[3].y);
        }

        private void ModifyInputsForEachPlayer()
        {
            if (m_playersInStack <= 1) return;
            for(int idx = 1;  idx < m_playersInStack; idx++)
            {
                m_playerInputs[idx] -= m_playerInputs[idx - 1];
            }
        }

        private void SetInputsForEachPlayer()
        {
            int playerIdx = 0;
            foreach(Player.Controller playerController in m_playerControllers)
            {
                if (playerController == null) break;

                m_playerInputs[playerIdx] = playerController.m_moveInput;

                playerIdx++;
            }
        }

        private void CountPlayersInStack()
        {
            if(m_playerControllers.Count == 0)
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