using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class PauseMenu : MonoBehaviour
    {
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
        [HideInInspector] public GameObject m_audioButton;
        [HideInInspector] public GameObject m_restartPopupNoButton;
        [HideInInspector] public GameObject m_quitPopupNoButton;
        [HideInInspector] public GameObject m_tabButtons;
        [HideInInspector] public GameObject m_gradeCard;
        [HideInInspector] public GameObject m_letterGrade;

        private void Awake()
        {
            m_pauseMenu = this.gameObject;
            m_pauseMenuContent = transform.Find("PauseMenuContent").gameObject;
            m_settingsMenu = transform.Find("SettingsMenu").gameObject;
            m_audioMenu = transform.Find("AudioMenu").gameObject;
            m_masterVolumeSlider = transform.Find("MasterVolumeSlider").gameObject;
            m_restartPopup = transform.Find("RestartPopup").gameObject;
            m_quitPopup = transform.Find("QuitPopup").gameObject;
            m_resumeButton = transform.Find("ResumeButton").gameObject;
            m_audioButton = transform.Find("AudioButton").gameObject;
            m_restartPopupNoButton = transform.Find("RestartPopupNoButton").gameObject;
            m_quitPopupNoButton = transform.Find("QuitPopupNoButton").gameObject;
            m_tabButtons = transform.Find("TabButtons").gameObject;
            m_gradeCard = transform.Find("GradeCard").gameObject;
            m_letterGrade = transform.Find("LetterGrade").gameObject;
        }

        public void CloseMenu(MENU menu)
        {
            switch (menu)
            {
                case MENU.Pause:
                    m_pauseMenu.SetActive(false);
                    break;
                case MENU.Settings:
                    m_settingsMenu.SetActive(false);
                    break;
                case MENU.Audio:
                    m_audioMenu.SetActive(false);
                    break;
                case MENU.Controls:
                    break;
                case MENU.Accessibility:
                    break;
                case MENU.PauseContent:
                    m_pauseMenuContent.SetActive(false);
                    m_gradeCard.SetActive(false);
                    break;
            }
        }

        public void OpenMenu(MENU menu)
        {
            switch (menu)
            {
                case MENU.Pause:
                    m_pauseMenu.SetActive(true);
                    break;
                case MENU.Settings:
                    m_settingsMenu.SetActive(true);
                    break;
                case MENU.Audio:
                    m_audioMenu.SetActive(true);
                    break;
                case MENU.Controls:
                    break;
                case MENU.Accessibility:
                    break;
                case MENU.PauseContent:
                    m_pauseMenuContent.SetActive(true);
                    m_gradeCard.SetActive(true);
                    break;
            }
        }

        public IEnumerator WaitThenCloseMenu(MENU menu)
        {
            yield return new WaitForFixedUpdate();

            CloseMenu(menu);
        }

        public void OnResumeButtonPressed()
        {
            StartCoroutine(WaitThenCloseMenu(MENU.Pause));
            StartCoroutine(WaitThenCloseMenu(MENU.PauseContent));
            TimeManager.instance.isGamePaused = false;
        }

        public void OnSettingsButtonPressed()
        {
            OpenMenu(MENU.Settings);
            CloseMenu(MENU.PauseContent);
            CloseMenu(MENU.Audio);
            m_tabButtons.SetActive(true);
            EventSystem.current.SetSelectedGameObject(m_audioButton);
        }

        public void OnRestartButtonPressed()
        {
            m_restartPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(m_restartPopupNoButton);
        }

        public void OnQuitButtonPressed()
        {
            m_quitPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(m_quitPopupNoButton);
        }

        public void OnNoRestartButtonPressed()
        {
            if (m_restartPopup.activeInHierarchy)
            {
                m_restartPopup.SetActive(false);
                EventSystem.current.SetSelectedGameObject(m_resumeButton);
            }

            if (m_quitPopup.activeInHierarchy)
            {
                m_quitPopup.SetActive(false);
                EventSystem.current.SetSelectedGameObject(m_resumeButton);
            }
        }

        public void OnYesRestartButtonPressed()
        {

        }

        public void OnAudioButtonPressed()
        {
            OpenMenu(MENU.Audio);
            CloseMenu(MENU.Settings);
            EventSystem.current.SetSelectedGameObject(m_masterVolumeSlider);
        }

        public void OnBackButtonPressed()
        {
            CloseMenu(MENU.Settings);
            OpenMenu(MENU.PauseContent);
            m_tabButtons.SetActive(false);
            EventSystem.current.SetSelectedGameObject(m_resumeButton);
        }

        public void OnControlsButtonPressed()
        {

        }

        public void OnAccessibilityButtonPressed()
        {

        }
    }
}
