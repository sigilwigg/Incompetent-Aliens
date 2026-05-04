using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    public CinemachineTargetGroup m_cinemachineTargetGroup;

    private int m_numberPlayersJoined = 0;
    private Player.Controller m_playerQuedForColorAssignment = null;
    [Header("Player Colors")]
    public List<Color> m_playerColors = new List<Color>();
    public List<PlayerInput> m_playerInputsJoined = new List<PlayerInput>();

    private void Update()
    {
        HandleNewConnectionsKeyboardWASD();
        HandleNewConnectionsKeyboardArrows();
        HandleNewConnectionsGamepads();
        HandleNewColorAssignments();
    }

    private void HandleNewConnectionsKeyboardWASD()
    {
        if (UIManager.instance != null && UIManager.instance.m_pauseMenu.activeInHierarchy) return;
        if (Keyboard.current == null) return;
        if(!m_isJoinedKeyboardWASD && Keyboard.current.spaceKey.wasPressedThisFrame && !MaxPlayerCountReached())
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

            // ----- handle camera retargeting -----
            if (m_cinemachineTargetGroup != null)
                m_cinemachineTargetGroup.AddMember(player.GetComponent<Player.Controller>().m_movement.transform, 1.0f, 0.0f);
            m_playerQuedForColorAssignment = player.GetComponent<Player.Controller>();
            m_numberPlayersJoined++;
        }
    }

    private void HandleNewConnectionsKeyboardArrows()
    {
        if (UIManager.instance != null && UIManager.instance.m_pauseMenu.activeInHierarchy) return;
        if (Keyboard.current == null) return;
        if (!m_isJoinedKeyboardArrows && Keyboard.current.rightShiftKey.wasPressedThisFrame && !MaxPlayerCountReached())
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

            // ----- handle camera retargeting -----
            if (m_cinemachineTargetGroup != null)
                m_cinemachineTargetGroup.AddMember(player.GetComponent<Player.Controller>().m_movement.transform, 1.0f, 0.0f);
            m_playerQuedForColorAssignment = player.GetComponent<Player.Controller>();
            m_numberPlayersJoined++;
        }
    }

    private void HandleNewConnectionsGamepads()
    {
        // ----- don't process if paused -----
        if (UIManager.instance != null && UIManager.instance.m_pauseMenu.activeInHierarchy) return;

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.startButton.wasPressedThisFrame && !MaxPlayerCountReached())
            {
                foreach (PlayerInput playerInput in m_playerInputsJoined)
                {
                    if (gamepad.deviceId == playerInput.devices[0].deviceId) return;
                }

                // ----- create player -----
                var player = PlayerInput.Instantiate(
                    m_playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: gamepad
                );

                // ----- set player to spawn point -----
                player.transform.position = m_spawnPoint.position;

                m_playerInputsJoined.Add(player);

                // ----- handle camera retargeting -----
                if (m_cinemachineTargetGroup != null)
                    m_cinemachineTargetGroup.AddMember(
                        player.GetComponent<Player.Controller>().m_movement.transform,
                        1.0f,
                        0.0f
                );
                m_playerQuedForColorAssignment = player.GetComponent<Player.Controller>();
                m_numberPlayersJoined++;
            }
        }
    }

    private void HandleNewColorAssignments()
    {
        if (m_playerQuedForColorAssignment == null) return;
        m_playerQuedForColorAssignment.SetColor(m_playerColors[m_numberPlayersJoined - 1]);
        m_playerQuedForColorAssignment = null;
    }

    private bool MaxPlayerCountReached()
    {
        return m_numberPlayersJoined >= 4 ? true : false;
    }
}
