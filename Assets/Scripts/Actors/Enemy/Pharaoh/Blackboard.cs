using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Blackboard : Enemy.AIBlackboard
    {
        public bool m_isMirrorHeldByPlayers = false;
        public bool m_isPharaohInMirrorArea = false;
        public bool m_isDistracted = false;
    }
}
