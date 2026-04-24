using System.Collections;
using UnityEngine;
using UserInterface;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public enum MENU
    {
        Pause,
        Settings,
        Audio,
        Controls,
        Accessibility,
        PauseContent
    }

    public GameObject m_pauseMenu;
    public GameObject m_pauseMenuContent;
    public GameObject m_settingsMenu;
    public GameObject m_audioMenu;
    public GameObject m_masterVolumeSlider;
    public GameObject m_restartPopup;
    public GameObject m_quitPopup;
    public GameObject m_resumeButton;
    public GameObject m_audioButton;
    public GameObject m_restartPopupNoButton;
    public GameObject m_quitPopupNoButton;
    public GameObject m_tabButtons;
    public GameObject m_gradeCard;
    public GameObject m_letterGrade;

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
        Transform pTransform = m_pauseMenu.transform;
        m_pauseMenuContent = FindTransform.FindChildNamed(pTransform, "PauseMenuContent").gameObject;
        m_settingsMenu = FindTransform.FindChildNamed(pTransform, "SettingsMenu").gameObject;
        m_audioMenu = FindTransform.FindChildNamed(pTransform, "AudioMenu").gameObject;
        m_masterVolumeSlider = FindTransform.FindChildNamed(pTransform, "MasterVolumeSlider").gameObject;
        m_restartPopup = FindTransform.FindChildNamed(pTransform, "RestartPopup").gameObject;
        m_quitPopup = FindTransform.FindChildNamed(pTransform, "QuitPopup").gameObject;
        m_resumeButton = FindTransform.FindChildNamed(pTransform, "ResumeButton").gameObject;
        m_audioButton = FindTransform.FindChildNamed(pTransform, "AudioButton").gameObject;
        m_restartPopupNoButton = FindTransform.FindChildNamed(pTransform, "RestartPopupNoButton").gameObject;
        m_quitPopupNoButton = FindTransform.FindChildNamed(pTransform, "QuitPopupNoButton").gameObject;
        m_tabButtons = FindTransform.FindChildNamed(pTransform, "TabButtons").gameObject;
        m_gradeCard = FindTransform.FindChildNamed(pTransform, "GradeCard").gameObject;
        m_letterGrade = FindTransform.FindChildNamed(pTransform, "LetterGrade").gameObject;
    }

    public void CloseMenu(MENU menu)
    {
        switch (menu)
        {
            case MENU.Pause:
                if(m_pauseMenu) m_pauseMenu.SetActive(false);
                break;
            case MENU.Settings:
                if (m_settingsMenu) m_settingsMenu.SetActive(false);
                break;
            case MENU.Audio:
                if (m_audioMenu) m_audioMenu.SetActive(false);
                break;
            case MENU.Controls:
                break;
            case MENU.Accessibility:
                break;
            case MENU.PauseContent:
                if (m_pauseMenuContent) m_pauseMenuContent.SetActive(false);
                if (m_gradeCard) m_gradeCard.SetActive(false);
                break;
        }
    }

    public void OpenMenu(MENU menu)
    {
        switch (menu)
        {
            case MENU.Pause:
                if (m_pauseMenu) m_pauseMenu.SetActive(true);
                break;
            case MENU.Settings:
                if (m_settingsMenu) m_settingsMenu.SetActive(true);
                break;
            case MENU.Audio:
                if (m_audioMenu) m_audioMenu.SetActive(true);
                break;
            case MENU.Controls:
                break;
            case MENU.Accessibility:
                break;
            case MENU.PauseContent:
                if (m_pauseMenuContent) m_pauseMenuContent.SetActive(true);
                if (m_gradeCard) m_gradeCard.SetActive(true);
                break;
        }
    }

    public IEnumerator WaitThenCloseMenu(MENU menu)
    {
        yield return new WaitForFixedUpdate();

        CloseMenu(menu);
    }
}
