using UnityEngine;
using UnityEngine.InputSystem;

/*
 *  Handles checking for new gamepad or keyboard connections.
 *  TODO: needs handling disconnections, reconnections
 *  
 *  HandleNewConnectionsKeyboardWASD()      => new wasd movement based keyboard connections (only 1 allowed)
 *  HandleNewConnectionsKeyboardArrows()    => new arrow based movement keyboard connections (only 1 allowed)
 *  HandleNewConnectionsGamepads()          => new gamepad connections (any number allowed)
 */

public class JoinManager : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private Transform m_spawnPoint;

    public bool m_isJoinedKeyboardWASD = false;
    public bool m_isJoinedKeyboardArrows = false;

    private void Update()
    {
        HandleNewConnectionsKeyboardWASD();
        HandleNewConnectionsKeyboardArrows();
        HandleNewConnectionsGamepads();
    }

    private void HandleNewConnectionsKeyboardWASD()
    {
        if (Keyboard.current == null) return;
        if(!m_isJoinedKeyboardWASD && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // ----- create player -----
            var player = PlayerInput.Instantiate(
                m_playerPrefab,
                controlScheme: "KeyboardWASD",
                pairWithDevice: Keyboard.current
            );

            // ----- set player to spawn point -----
            player.transform.position = m_spawnPoint.position;

            // ----- remember joined -----
            m_isJoinedKeyboardWASD = true;
        }
    }

    private void HandleNewConnectionsKeyboardArrows()
    {
        if (Keyboard.current == null) return;
        if (!m_isJoinedKeyboardArrows && Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            // ----- create player -----
            var player = PlayerInput.Instantiate(
                m_playerPrefab,
                controlScheme: "KeyboardArrows",
                pairWithDevice: Keyboard.current
            );

            // ----- set player to spawn point -----
            player.transform.position = m_spawnPoint.position;

            // ----- remember joined -----
            m_isJoinedKeyboardArrows = true;
        }
    }

    private void HandleNewConnectionsGamepads()
    {
        foreach(Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                // ----- create player -----
                var player = PlayerInput.Instantiate(
                    m_playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: gamepad
                );

                // ----- set player to spawn point -----
                player.transform.position = m_spawnPoint.position;
            }
        }
    }
}
