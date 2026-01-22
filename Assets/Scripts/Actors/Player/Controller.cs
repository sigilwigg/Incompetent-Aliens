using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Player.Movement m_movement;

        public bool m_canMove;

        private void Awake()
        {
            m_movement = GetComponentInChildren<Player.Movement>();
        }
    }
}