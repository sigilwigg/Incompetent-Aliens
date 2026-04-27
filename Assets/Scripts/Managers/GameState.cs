using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    private LevelData m_levelData;
    public string m_levelNameCompleted;

    public enum SceneType
    {
        MainMenu,
        Level,
        LevelComplete
    }
    public SceneType m_currentSceneType;

    public bool m_isLevelComplete = false;
    public bool m_shouldReset = false;

    public string m_recordedLevelGrade = "F";
    private Timer m_timer;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_timer = GameObject.FindWithTag("GameTimer").GetComponent<Timer>();
    }

    public void RestartLevel()
    {
        SceneController.ReloadScene();
    }

    private void Update()
    {
        if (m_shouldReset) RestartLevel();
        if (m_isLevelComplete) ToLevelCompleteScreen();
    }

    private void ToLevelCompleteScreen()
    {
        m_levelData = GameObject.FindWithTag("LevelData").GetComponent<LevelData>();
        m_recordedLevelGrade = m_levelData.EvaluateGrade(m_timer.GetMS());
        m_levelNameCompleted = m_levelData.m_levelName;
        m_currentSceneType = SceneType.LevelComplete;
        SceneController.CallTransitionToScene("LevelComplete");
        m_isLevelComplete = false;
    }
}
