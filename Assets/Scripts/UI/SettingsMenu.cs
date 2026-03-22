using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public void OnAudioButtonPressed()
    {
        UIManager.instance.OpenMenu(UIManager.MENU.Audio);
        UIManager.instance.CloseMenu(UIManager.MENU.Settings);
        EventSystem.current.SetSelectedGameObject(UIManager.instance.masterVolumeSlider);
    }

    public void OnControlsButtonPressed()
    {

    }

    public void OnAccessibilityButtonPressed()
    {

    }
}
