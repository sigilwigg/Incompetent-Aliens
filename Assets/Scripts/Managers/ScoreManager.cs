using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Timer m_timer;
    private GameState m_gameState;

    public float m_timeGrade1;
    public float m_timeGrade2;
    public float m_timeGrade3;

    private void Start()
    {
        m_timer = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<Timer>();
        m_gameState = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameState>();
    }

    private void Update()
    {
        if (m_gameState.m_isLevelComplete == true) EvaluateScore();
    }

    private void EvaluateScore()
    {
        int finalScore = 0;
        if (m_timer.m_timeElapsed >= m_timeGrade1) finalScore = 1;
        if (m_timer.m_timeElapsed >= m_timeGrade2) finalScore = 2;
        if (m_timer.m_timeElapsed >= m_timeGrade3) finalScore = 3;
        Debug.Log(finalScore);
    }
}
