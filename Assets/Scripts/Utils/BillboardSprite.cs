using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        transform.rotation = Quaternion.Euler(
            40.0f,
            0.0f,
            transform.rotation.eulerAngles.z
            );
    }
}
