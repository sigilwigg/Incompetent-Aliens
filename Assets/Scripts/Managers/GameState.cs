using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool m_isLevelComplete = false;
    public bool m_shouldReset = false;

    public void RestartLevel()
    {
        SceneController.ReloadScene();
    }

    private void Update()
    {
        if (m_shouldReset) RestartLevel();
    }
}
