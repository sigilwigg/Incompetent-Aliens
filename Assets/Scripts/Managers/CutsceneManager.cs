using Player;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private Controller m_playerController;

    public bool isCutscenePlaying;

    private void Start()
    {
        m_playerController = GameObject.FindWithTag("Player").GetComponent<Controller>();
    }

    public void StartCutscene()
    {
        isCutscenePlaying = true;

        // Additional logic to start the cutscene, such as playing animations, disabling player controls, etc.
        m_playerController.m_canMove = false;

    }

    public void EndCutscene()
    {
        isCutscenePlaying = false;

        // Additional logic to end the cutscene, such as re-enabling player controls, transitioning back to gameplay, etc.
        m_playerController.m_canMove = true;
    }
}
