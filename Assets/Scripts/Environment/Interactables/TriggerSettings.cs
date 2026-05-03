using Player;
using UnityEngine;

/*
 *  An interactable that specifically prompts the settings menu.
 *  Used in main menu scene only.
 */

namespace Interactables
{
    public class TriggerSettings : Interactable
    {
        private void Start()
        {
        }

        public override void Interact(Player.Controller playerController)
        {
            if (TimeManager.instance.isGamePaused)
            {
                TimeManager.instance.isGamePaused = false;
                UIManager.instance.CloseMenu(UIManager.MENU.Pause);
            }
            else
            {
                TimeManager.instance.isGamePaused = true;
                UIManager.instance.OpenMenu(UIManager.MENU.Pause);
                UIManager.instance.OpenMenu(UIManager.MENU.PauseContent);
            }
        }
    }

}
