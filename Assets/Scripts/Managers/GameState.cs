using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public enum SceneType
    {
        MainMenu,
        Level,
        LevelComplete
    }
    public SceneType m_currentSceneType;

    public bool m_isLevelComplete = false;
    public bool m_shouldReset = false;

    public float m_recordedLevelTime;

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
        m_currentSceneType = SceneType.LevelComplete;
        //m_recordedLevelTime = TimeManager.instance.m
        SceneController.CallTransitionToScene("LevelComplete");
        m_isLevelComplete = false;
    }
}
