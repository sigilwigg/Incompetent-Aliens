using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class PauseMenu : MonoBehaviour
    {
        public void OnResumeButtonPressed()
        {
            StartCoroutine(UIManager.instance.WaitThenCloseMenu(UIManager.MENU.Pause));
            StartCoroutine(UIManager.instance.WaitThenCloseMenu(UIManager.MENU.PauseContent));
            TimeManager.instance.isGamePaused = false;
        } 

        public void OnSettingsButtonPressed()
        {
            UIManager.instance.OpenMenu(UIManager.MENU.Settings);
            UIManager.instance.CloseMenu(UIManager.MENU.PauseContent);
            UIManager.instance.CloseMenu(UIManager.MENU.Audio);
            UIManager.instance.m_tabButtons.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.m_audioButton);
        }

        public void OnRestartButtonPressed()
        {
            UIManager.instance.m_restartPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.m_restartPopupNoButton);
        }

        public void OnRestartYesButtonPressed()
        {
            GameState gameStateManager = GameObject.FindWithTag("GameStateManager").GetComponent<GameState>();
            gameStateManager.RestartLevel();
            UIManager.instance.m_restartPopup.SetActive(false);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.m_resumeButton);
            OnResumeButtonPressed();
        }

        public void OnQuitButtonPressed()
        {
            UIManager.instance.m_quitPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.m_quitPopupNoButton);
        }

        public void OnNoButtonPressed()
        {
            if (UIManager.instance.m_restartPopup.activeInHierarchy)
            {
                UIManager.instance.m_restartPopup.SetActive(false);
                EventSystem.current.SetSelectedGameObject(UIManager.instance.m_resumeButton);
            }

            if (UIManager.instance.m_quitPopup.activeInHierarchy) 
            {
                UIManager.instance.m_quitPopup.SetActive(false);
                EventSystem.current.SetSelectedGameObject(UIManager.instance.m_resumeButton);
            }
        }

        public void OnYesButtonPressed()
        {

        }
    }

}
