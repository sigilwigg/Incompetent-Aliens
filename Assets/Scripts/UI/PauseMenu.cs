using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class PauseMenu : MonoBehaviour
    {
        public void PauseGame()
        {
            TimeManager.instance.isGamePaused = true;
        }

        public void OnResumeButtonPressed()
        {
            StartCoroutine(UIManager.instance.WaitThenCloseMenu(UIManager.MENU.Pause));
            TimeManager.instance.isGamePaused = false;
        } 

        public void OnSettingsButtonPressed()
        {
            UIManager.instance.OpenMenu(UIManager.MENU.Settings);
            UIManager.instance.pauseMenuContent.SetActive(false);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.audioButton);
        }

        public void OnRestartButtonPressed()
        {
            UIManager.instance.restartPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.restartPopupNoButton);
        }

        public void OnQuitButtonPressed()
        {
            UIManager.instance.quitPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.quitPopupNoButton);
        }

        public void OnNoButtonPressed()
        {
            if (UIManager.instance.restartPopup.activeInHierarchy)
            {
                UIManager.instance.restartPopup.SetActive(false);
                EventSystem.current.SetSelectedGameObject(UIManager.instance.resumeButton);
            }

            if (UIManager.instance.quitPopup.activeInHierarchy) 
            {
                UIManager.instance.quitPopup.SetActive(false);
                EventSystem.current.SetSelectedGameObject(UIManager.instance.resumeButton);
            }
        }

        public void OnYesButtonPressed()
        {

        }

        public void CloseSettingsMenus()
        {

        }
    }

}
