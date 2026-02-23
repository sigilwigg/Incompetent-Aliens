using UnityEngine;

namespace Enemy
{
    public class AIBlackboard : MonoBehaviour
    {
        [Header("Navigation")]
        public Path m_path;

        [Header("Default AI Parameters")]
        public bool m_canSeePlayer = false;

        [Header("References")]
        public Transform[] m_players;

        protected virtual void Start()
        {
            //----- If array isn't set in inspector, try to find all Player objects in the scene -----
            if(m_players == null || m_players.Length == 0)
            {
                GameObject[] found = GameObject.FindGameObjectsWithTag("Player");
                if(found.Length == 0)
                {
                    Debug.LogError("Player not found in scene. Make sure there is a GameObject with the tag 'Player' in the scene.");
                    return;
                }

                m_players = new Transform[found.Length];
                for(int i = 0;i < found.Length;i++)
                {
                    m_players[i] = found[i].transform;
                }

                return;
            }
        }

    }
}
