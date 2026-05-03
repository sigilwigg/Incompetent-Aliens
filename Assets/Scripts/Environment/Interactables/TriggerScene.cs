using Player;
using UnityEngine;

/*
 *  An interactable that calls a transition to named scene wasn interacted with.
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
