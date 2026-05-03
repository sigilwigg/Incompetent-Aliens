using UnityEngine;
using Player;

/*
 *  Ensures glyph is dropped on the ground when the player is falling / not grounded.
 *  
 *      DetectFall() => is the player grounded.
 */

public class DropWhileFalling : MonoBehaviour
{
    private Controller m_player;
    private void Awake()
    {
        m_player = GetComponentInParent<Controller>();
    }

    void Update()
    {
        DetectFall();
    }
    private void DetectFall()
    {
        if (!m_player.m_movement.m_isGrounded)
        {
            m_player.DropItem();
        }
    }
}
