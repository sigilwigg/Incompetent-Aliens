using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public void OnAudioButtonPressed()
    {
        UIManager.instance.OpenMenu(UIManager.MENU.Audio);
        UIManager.instance.CloseMenu(UIManager.MENU.Settings);
        EventSystem.current.SetSelectedGameObject(UIManager.instance.m_masterVolumeSlider);
    }

    public void OnBackButtonPressed()
    {
        UIManager.instance.CloseMenu(UIManager.MENU.Settings);
        UIManager.instance.OpenMenu(UIManager.MENU.PauseContent);
        UIManager.instance.m_tabButtons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(UIManager.instance.m_resumeButton);
    }

    public void OnControlsButtonPressed()
    {
        UIManager.instance.OpenMenu(UIManager.MENU.Controls);
        UIManager.instance.CloseMenu(UIManager.MENU.Settings);
    }

    public void OnAccessibilityButtonPressed()
    {

    }
}
