using UnityEngine;

/*
 *  Simple script for decting i f the mirror was put in place for distracting the Pharoh
 */

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
