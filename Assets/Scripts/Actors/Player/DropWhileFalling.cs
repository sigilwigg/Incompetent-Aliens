using UnityEngine;
using Player;

public class DropWhileFalling : MonoBehaviour
{
    private Player.Controller m_player;


    private void Awake()
    {
        m_player = GetComponentInParent<Player.Controller>();
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
            DropWhenFalling();
            Debug.Log("Falling");
        }
    }
    private void DropWhenFalling()
    {
        m_player.DropItem();
    }
}
