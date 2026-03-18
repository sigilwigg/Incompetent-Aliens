using UnityEngine;

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

        } 

        public void OnSettingsButtonPressed()
        {

        }

        public void OnRestartButtonPressed()
        {

        }

        public void OnQuitButtonPressed()
        {

        }
    }

}
