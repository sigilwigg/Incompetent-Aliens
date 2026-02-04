using UnityEngine;
using Cinemachine;

namespace Camera
{
    public class CameraConfiner : MonoBehaviour
    {
        private CinemachineConfiner m_confiner;
        public Collider m_SceneBoundingVolume;
        public float m_SceneSlowingDistance;

        private void Start()
        {
            m_confiner = new CinemachineConfiner();
            m_confiner.m_BoundingVolume = m_SceneBoundingVolume;
            m_confiner.m_Damping = m_SceneSlowingDistance;
        }

    }
}
