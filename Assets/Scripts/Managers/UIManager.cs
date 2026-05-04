using System.Collections;
using UnityEngine;
using UserInterface;
using SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [HideInInspector] public GameObject m_pauseMenuContent;
    [HideInInspector] public GameObject m_settingsMenu;
    [HideInInspector] public GameObject m_audioMenu;
    [HideInInspector] public GameObject m_controlsMenu;
    [HideInInspector] public GameObject m_masterVolumeSlider;
    [HideInInspector] public GameObject m_restartPopup;
    [HideInInspector] public GameObject m_quitPopup;
    [HideInInspector] public GameObject m_resumeButton;
    [HideInInspector] public GameObject m_audioButton;
    [HideInInspector] public GameObject m_restartPopupNoButton;
    [HideInInspector] public GameObject m_quitPopupNoButton;
    [HideInInspector] public GameObject m_tabButtons;
    [HideInInspector] public GameObject m_gradeCard;
    [HideInInspector] public GameObject m_letterGrade;
    [HideInInspector] public Button m_restartButton;
    [HideInInspector] public Button m_settingsButton;

    public GameObject m_lvlCompMenu;
    public GameObject m_lvlCompRestartPopup;
    public GameObject m_lvlCompQuitPopup;
    public GameObject m_lvlCompQuitPopupNoButton;

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

        //DontDestroyOnLoad(gameObject);    
    }

    private void Start()
    {
        SetUpComponents();
    }

    public void SetUpComponents()
    {
        // ----- pause menu components -----
        Transform pTransform = m_pauseMenu.transform;
        m_pauseMenuContent = FindTransform.FindChildNamed(pTransform, "PauseMenuContent").gameObject;
        m_settingsMenu = FindTransform.FindChildNamed(pTransform, "SettingsMenu").gameObject;
        m_audioMenu = FindTransform.FindChildNamed(pTransform, "AudioMenu").gameObject;
        m_controlsMenu = FindTransform.FindChildNamed(pTransform, "ControlsMenu").gameObject;
        m_masterVolumeSlider = FindTransform.FindChildNamed(pTransform, "MasterVolumeSlider").gameObject;
        m_restartPopup = FindTransform.FindChildNamed(pTransform, "RestartPopup").gameObject;
        m_quitPopup = FindTransform.FindChildNamed(pTransform, "QuitPopup").gameObject;
        m_resumeButton = FindTransform.FindChildNamed(pTransform, "ResumeButton").gameObject;
        m_audioButton = FindTransform.FindChildNamed(pTransform, "AudioButton").gameObject;
        m_restartPopupNoButton = FindTransform.FindChildNamed(pTransform, "RestartPopupNoButton").gameObject;
        m_quitPopupNoButton = FindTransform.FindChildNamed(pTransform, "QuitPopupNoButton").gameObject;
        m_tabButtons = FindTransform.FindChildNamed(pTransform, "TabButtons").gameObject;
        m_restartButton = FindTransform.FindChildNamed(pTransform, "RestartButton").gameObject.GetComponent<Button>();
        m_settingsButton = FindTransform.FindChildNamed(pTransform, "SettingsButton").gameObject.GetComponent<Button>();

        if (SceneManager.GetActiveScene().name == "Egypt_1")
        {
            m_letterGrade = FindTransform.FindChildNamed(pTransform, "LetterGrade").gameObject;
            m_gradeCard = FindTransform.FindChildNamed(pTransform, "GradeCard").gameObject;
            m_restartButton.interactable = true;

        }
        else
        {
            m_restartButton.interactable = false;
        }

        // ----- lvl complete menu components -----
        m_lvlCompMenu = GameObject.FindWithTag("LvlCompMenu");
        if (m_lvlCompMenu == null) return;
        Transform lvlCompTransform = m_lvlCompMenu.transform;
        m_lvlCompRestartPopup = FindTransform.FindChildNamed(lvlCompTransform, "RestartPopup").gameObject;
        m_lvlCompQuitPopup = FindTransform.FindChildNamed(lvlCompTransform, "QuitPopup").gameObject;
        m_lvlCompQuitPopupNoButton = FindTransform.FindChildNamed(lvlCompTransform, "QuitPopupNoButton").gameObject;
    }

    public void CloseMenu(MENU menu)
    {
        switch (menu)
        {
            case MENU.Pause:
                if(m_pauseMenu) m_pauseMenu.SetActive(false);
                CloseAllPopUps();
                break;
            case MENU.Settings:
                if (m_settingsMenu) m_settingsMenu.SetActive(false);
                break;
            case MENU.Audio:
                if (m_audioMenu) m_audioMenu.SetActive(false);
                break;
            case MENU.Controls:
                if (m_controlsMenu) m_controlsMenu.SetActive(false);
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
                if (m_pauseMenu)
                {
                    m_pauseMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(m_resumeButton);
                }
                break;
            case MENU.Settings:
                if (m_settingsMenu) m_settingsMenu.SetActive(true);
                break;
            case MENU.Audio:
                if (m_audioMenu) m_audioMenu.SetActive(true);
                break;
            case MENU.Controls:
                if (m_controlsMenu) m_controlsMenu.SetActive(true);
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

    public IEnumerator WaitThenOpenMenu(MENU menu)
    {
        yield return new WaitForFixedUpdate();

        OpenMenu(menu);
    }

    public void CloseAllPopUps()
    {
        m_restartPopup.SetActive(false);
        m_quitPopup.SetActive(false);
    }
}
