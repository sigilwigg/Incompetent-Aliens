using UnityEngine;

public class LockToPosition : MonoBehaviour
{
    public Transform m_targetTransform;

    private void Update()
    {
        if(m_targetTransform == null) return;
        transform.position = m_targetTransform.position;
    }
}
