using Cinemachine;
using Player;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private CinemachineBrain m_cinemachineBrain;
    private Controller m_playerController;

    [Header("Cutscene Status")]
    public bool isCutscenePlaying;

    private void Start()
    {
        m_playerController = GameObject.FindWithTag("Player").GetComponent<Controller>();
        m_cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        DetectBlend();
    }

    public void StartCutscene()
    {
        isCutscenePlaying = true;
        
        //stops player movement when cut scene starts.
        m_playerController.m_canMove = false;

    }

    public void EndCutscene()
    {
        isCutscenePlaying = false;

        //allows player movement when cut scene ends.
        m_playerController.m_canMove = true;
    }

    //starts the cutscene when a blend starts, and ends the cutscene when the blend ends.
    private void DetectBlend()
    {
        if(m_cinemachineBrain.IsBlending)
        {
            if(!isCutscenePlaying)
            {
                StartCutscene();
            }
        }
        else
        {
            if(isCutscenePlaying)
            {
                EndCutscene();
            }
        }
    }
}
