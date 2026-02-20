using UnityEngine;

namespace Enemy
{
    public class AIBlackboard : MonoBehaviour
    {
        [Header("Navigation")]
        public Path m_path;

        [Header("Default AI Parameters")]
        public bool m_canSeePlayer = false;
       
    }
}
