using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class LevelCompleteMenu : MonoBehaviour
    {
        public void OnMainMenuButtonPressed()
        {
            SceneController.CallTransitionToScene("MainMenu");
        }

        public void OnRestartButtonPressed()
        {
            UIManager.instance.m_lvlCompRestartPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.m_restartPopupNoButton);
        }

        public void OnRestartYesButtonPressed()
        {
            GameState gameStateManager = GameObject.FindWithTag("GameStateManager").GetComponent<GameState>();
            SceneController.CallTransitionToScene(gameStateManager.m_levelNameCompleted);
            UIManager.instance.m_lvlCompRestartPopup.SetActive(false);
        }

        public void OnQuitButtonPressed()
        {
            UIManager.instance.m_lvlCompQuitPopup.SetActive(true);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.m_lvlCompQuitPopupNoButton);
        }

        public void OnNoButtonPressed()
        {
            if (UIManager.instance.m_lvlCompRestartPopup.activeInHierarchy)
            {
                UIManager.instance.m_lvlCompRestartPopup.SetActive(false);
            }

            if (UIManager.instance.m_lvlCompQuitPopup.activeInHierarchy)
            {
                UIManager.instance.m_lvlCompQuitPopup.SetActive(false);
            }
        }

        public void OnYesButtonPressed()
        {

        }
    }

}
