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

    public GameObject pauseMenu;
    public GameObject pauseMenuContent;
    public GameObject settingsMenu;
    public GameObject audioMenu;
    public GameObject masterVolumeSlider;
    public GameObject restartPopup;
    public GameObject quitPopup;
    public GameObject resumeButton;
    public GameObject audioButton;
    public GameObject restartPopupNoButton;
    public GameObject quitPopupNoButton;

   

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

    public void CloseMenu(MENU menu)
    {
        switch (menu)
        {
            case MENU.Pause:
                pauseMenuContent.SetActive(false);
                break;
            case MENU.Settings:
                settingsMenu.SetActive(false);
                break;
            case MENU.Audio:
                audioMenu.SetActive(false);
                break;
            case MENU.Controls:
                break;
            case MENU.Accessibility:
                break;
            case MENU.PauseContent:
                pauseMenuContent.SetActive(true);
                break;
        }
    }

    public void OpenMenu(MENU menu)
    {
        switch (menu)
        {
            case MENU.Pause:
                pauseMenu.SetActive(true);
                break;
            case MENU.Settings:
                settingsMenu.SetActive(true);
                break;
            case MENU.Audio:
                audioMenu.SetActive(true);
                break;
            case MENU.Controls:
                break;
            case MENU.Accessibility:
                break;
            case MENU.PauseContent:
                pauseMenuContent.SetActive(true);
                break;
        }
    }

    public IEnumerator WaitThenCloseMenu(MENU menu)
    {
        yield return new WaitForFixedUpdate();

        CloseMenu(menu);
    }
}
