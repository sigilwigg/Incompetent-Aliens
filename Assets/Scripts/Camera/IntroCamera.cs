using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;

public class IntroCamera : MonoBehaviour
{
    public PlayerInputManager m_playerInputManager;
    public CinemachineVirtualCamera[] m_cameras;

    public CinemachineVirtualCamera m_GlyphCamera;
    public CinemachineVirtualCamera m_sarcophagusCamera;
    public CinemachineVirtualCamera m_gameCamera;

    public CinemachineVirtualCamera m_startCamera;
    private CinemachineVirtualCamera m_currentCamera;

    public GameObject m_ufo;

    private Vector3 newSpawn = new Vector3(0.05f, 2.5f, -5f);

    private void Start()
    {
        m_currentCamera = m_startCamera;

        for (int i = 0; i < m_cameras.Length; i++)
        {
            if (m_cameras[i] == m_currentCamera)
            {
                m_cameras[i].Priority = 20;
            }
            else
            {
                m_cameras[i].Priority = 10;
            }
        }

        StartCoroutine(PlayIntro());
    }

    public void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        m_currentCamera = newCamera;

        m_currentCamera.Priority = 20;

        for (int i = 0; i < m_cameras.Length; i++)
        {
            if (m_cameras[i] != m_currentCamera)
            {
                m_cameras[i].Priority = 10;
            }
        }
    }

    public IEnumerator PlayIntro()
    {
        m_playerInputManager.enabled = false;

        yield return new WaitForSeconds(3);

        SwitchCamera(m_sarcophagusCamera);

        yield return new WaitForSeconds(3);

        SwitchCamera(m_gameCamera);

        yield return new WaitForSeconds(1.5f);

        m_ufo.SetActive(true);
        m_playerInputManager.enabled = true;

        yield return new WaitForSeconds(1.5f);

        m_ufo.SetActive(false);
        
    }

}
