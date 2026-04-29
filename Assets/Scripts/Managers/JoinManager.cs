using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Player;
using System.Collections;
using System.Linq;
using UnityEngine.InputSystem.Users;

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
    public static JoinManager instance;

    [SerializeField] private GameObject m_playerPrefab;
    public Transform m_spawnPoint;

    public bool m_isJoinedKeyboardWASD = false;
    public bool m_isJoinedKeyboardArrows = false;

    public CinemachineTargetGroup m_cinemachineTargetGroup;

    private int m_numberPlayersJoined = 0;
    private Player.Controller m_playerQuedForColorAssignment = null;
    [Header("Player Colors")]
    public List<Color> m_playerColors = new List<Color>();
    public List<PlayerInput> m_playerInputsJoined = new List<PlayerInput>();
    public List<InputDevice> m_playerGamepads = new List<InputDevice>();
    public List<InputUser> m_playerUsers = new List<InputUser>();

    public GameObject m_bouncingBallPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_cinemachineTargetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
    }

    private void Update()
    {
        HandleNewConnectionsKeyboardWASD();
        HandleNewConnectionsKeyboardArrows();
        HandleNewConnectionsGamepads();
        HandleNewColorAssignments();
    }

    private void HandleNewConnectionsKeyboardWASD()
    {
        if (UIManager.instance != null && UIManager.instance.pauseMenu.activeInHierarchy) return;
        if (Keyboard.current == null) return;
        if (!m_isJoinedKeyboardWASD && Keyboard.current.spaceKey.wasPressedThisFrame && !MaxPlayerCountReached())
        {
            // ----- create player -----
            var player = PlayerInput.Instantiate(
                m_playerPrefab,
                controlScheme: "KeyboardWASD",
                pairWithDevice: Keyboard.current
            );

            // ----- set player to spawn point -----
            player.transform.position = m_spawnPoint.position;
            m_playerInputsJoined.Add(player);

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
        if (UIManager.instance != null && UIManager.instance.pauseMenu.activeInHierarchy) return;
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

            m_playerInputsJoined.Add(player);

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
        if (UIManager.instance != null && UIManager.instance.pauseMenu.activeInHierarchy) return;
        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !MaxPlayerCountReached())
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

                m_playerGamepads.Add(gamepad);
                m_playerUsers.Add(player.user);

                Debug.Log(player.devices[0].deviceId);

                // ----- set player to spawn point -----
                player.transform.position = m_spawnPoint.position;

                m_playerInputsJoined.Add(player);

                // ----- handle camera retargeting -----
                if (m_cinemachineTargetGroup != null)
                    m_cinemachineTargetGroup.AddMember(player.GetComponent<Player.Controller>().m_movement.transform, 1.0f, 0.0f);
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

    public void RespawnPlayers()
    {
        for (int i = 0; i < m_playerInputsJoined.Count; i++)
        {
            PlayerInput player = m_playerInputsJoined[i];
            Controller playerController = player.gameObject.GetComponent<Controller>();

            InputUser.PerformPairingWithDevice(m_playerGamepads[i], m_playerUsers[i]);

            ThrowPlayers(playerController);

            player.transform.position = m_spawnPoint.position;

            if (m_cinemachineTargetGroup != null)
                m_cinemachineTargetGroup.AddMember(player.GetComponent<Player.Controller>().m_movement.transform, 1.0f, 0.0f);
        }
    }

    public void ThrowPlayers(Player.Controller playerController, float bounceForce = 0.1f)
    {
        if (playerController.m_myStackController.m_playersInStack > 1)
        {
            for (int idx = 0; idx < playerController.m_myStackController.m_playersInStack; idx++)
            {
                StartCoroutine(ThrowPlayerCoroutine(playerController.m_myStackController.m_playerControllers[idx], bounceForce));
                playerController.m_myStackController.RemoveFromStack(idx);
            }
        }
        else
        {
            StartCoroutine(ThrowPlayerCoroutine(playerController, bounceForce));
        }
    }

    public IEnumerator ThrowPlayerCoroutine(Player.Controller playerController, float bounceForce = 0.1f)
    {
        //----- get references player model -----
        GameObject playerModel = playerController.m_playerModel;

        //----- throw player -----
        int RandomfallDirectionX = Random.Range(-1, 2); //returns ints -1, 0 and 1
        int RandomfallDirectionY = Random.Range(-1, 2); //returns ints -1, 0 and 1
        if ((RandomfallDirectionX == 0) && (RandomfallDirectionY == 0)) RandomfallDirectionX = 1;

        GameObject bouncePositioner = Instantiate(m_bouncingBallPrefab, transform.position, Quaternion.identity);
        BouncyBall bouncyBall = bouncePositioner.GetComponent<BouncyBall>();
        bouncyBall.SetPropelSelf(new Vector3(0.5f * RandomfallDirectionX, 0.2f, 0.5f * RandomfallDirectionY), bounceForce);

        playerController.SetBeingThrown(bouncePositioner.transform);
        yield return null;
    }
}
