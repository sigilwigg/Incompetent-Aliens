using UnityEngine;

public class LockGrabPoint : MonoBehaviour
{
    public Vector3 originalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = originalPosition;
    }
}
