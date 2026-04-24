using UnityEngine;

namespace Enemy.Pharaoh
{
    public class Blackboard : Enemy.AIBlackboard
    {
        [Header("Pharaoh Paramaters")]
        public bool m_isMirrorInMirrorZone = false;
        public bool m_isPharaohInMirrorZone = false;
        public bool m_isInSleepZone = false;
        public bool m_canCatchPlayer = false;
        public bool m_isDistracted = false;
    }
}
