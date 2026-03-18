using Player;
using UnityEngine;

/*
 * Interactables that can be picked up by the player.
 * 
 * Interact()       => standard interact override method, handles what to do for interact.
 * PositionItem()   => positions item in player hold position initially.
 */

namespace Interactables
{
    public class TriggerScene : Interactable
    {
        public string m_sceneName;

        private void Start()
        {
        }

        public override void Interact(Player.Controller playerController)
        {
            base.Interact(playerController);
            SceneController.CallTransitionToScene(m_sceneName);
        }
    }

}
