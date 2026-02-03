using UnityEngine;

public class Offset : MonoBehaviour
{
    public Player.Movement m_movement;

    private void Update()
    {
        transform.localPosition = new Vector3(
            2.0f * m_movement.m_movementInput.x,
            transform.localPosition.y,
            2.0f * m_movement.m_movementInput.z
        );
    }
}
