using UnityEngine;
using Cinemachine;

public class CameraSwitchRoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_firstVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera m_secondVirtualCamera;

    [SerializeField] private bool m_testSwitch;

    private void Update()
    {
        if(m_testSwitch)
        {
            SwitchCameras(m_firstVirtualCamera,m_secondVirtualCamera);
        }
        else
        {
            SwitchCameras(m_secondVirtualCamera,m_firstVirtualCamera);
        }
    }

    private void SwitchCameras(CinemachineVirtualCamera firstCamera, CinemachineVirtualCamera secondCamera)
    {
        firstCamera.Priority = 1;
        secondCamera.Priority = 0;

    }   

    
}
