using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public UserInterface.PauseMenu m_pauseMenu;
    //public UserInterface.LevelCompleteMenu m_levelCompleteMenu;

    public bool m_isPauseMenuEnabled;
    public bool m_isLevelCompleteMenuEnabled;

    private bool m_isPaused;

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

    public void PauseGame()
    {
        if (!m_isPauseMenuEnabled) return;
        if (m_isPaused) return;
        Debug.Log("pause");
        m_pauseMenu.OpenMenu(UserInterface.PauseMenu.MENU.Pause);
        m_pauseMenu.OpenMenu(UserInterface.PauseMenu.MENU.PauseContent);
        TimeManager.instance.isGamePaused = true;

        m_isPaused = true;
    }

    public void UnPauseGame()
    {
        if (!m_isPauseMenuEnabled) return;
        if(!m_isPaused) return;
        Debug.Log("unpause");
        m_pauseMenu.CloseMenu(UserInterface.PauseMenu.MENU.Pause);
        TimeManager.instance.isGamePaused = false;

        m_isPaused = false;
    }

    public void PauseMenuBack()
    {
        if (!m_isPauseMenuEnabled) return;
        if (!m_isPaused) return;

        if (m_pauseMenu.m_settingsMenu.activeInHierarchy)
        {
            m_pauseMenu.CloseMenu(UserInterface.PauseMenu.MENU.Settings);
            m_pauseMenu.OpenMenu(UserInterface.PauseMenu.MENU.Pause);
            m_pauseMenu.OpenMenu(UserInterface.PauseMenu.MENU.PauseContent);
            EventSystem.current.SetSelectedGameObject(m_pauseMenu.m_resumeButton);
            m_pauseMenu.m_tabButtons.SetActive(false);
        }
        if (m_pauseMenu.m_audioMenu.activeInHierarchy)
        {
            StartCoroutine(m_pauseMenu.WaitThenCloseMenu(UserInterface.PauseMenu.MENU.Audio));
            m_pauseMenu.OpenMenu(UserInterface.PauseMenu.MENU.Settings);
            EventSystem.current.SetSelectedGameObject(m_pauseMenu.m_audioButton);
        }
    }
}
