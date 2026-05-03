using UnityEngine;

/*
 *  Hotfix for a bug where the grab points on a muti-point pickup kept moving.
 */

public class LockGrabPoint : MonoBehaviour
{
    public Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = originalPosition;
    }
}
