using UnityEngine;


public class MirrorInMirrorZone : MonoBehaviour
{
    public bool m_isMirrorInPlace = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MirrorZone"))
        {
            m_isMirrorInPlace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MirrorZone"))
        {
            m_isMirrorInPlace = false;  
        }
    }
}
