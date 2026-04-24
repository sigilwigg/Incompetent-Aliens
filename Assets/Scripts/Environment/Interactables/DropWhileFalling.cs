using UnityEngine;
using Player;

public class DropWhileFalling : MonoBehaviour
{
    private Controller m_player;
    private void Awake()
    {
        m_player = GetComponentInParent<Controller>();
    }
    // Update is called once per frame
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
