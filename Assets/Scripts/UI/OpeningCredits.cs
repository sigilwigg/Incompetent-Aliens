using UnityEngine;

public class OpeningCredits : MonoBehaviour
{
    float m_timer = 0.0f;
    public float m_duration = 13.0f;
    bool m_sceneTransitionCalled = false;
    public string m_nextScene;

    private void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_duration && !m_sceneTransitionCalled)
        {
            SceneController.CallTransitionToScene(m_nextScene);
            m_sceneTransitionCalled = true;
        }
    }
}
