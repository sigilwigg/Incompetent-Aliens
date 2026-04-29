using Player;
using UnityEngine;
using UnityEngine.InputSystem;

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

            Debug.Log(JoinManager.instance.m_playerInputsJoined);

            foreach (PlayerInput player in JoinManager.instance.m_playerInputsJoined)
            {
                Player.Controller pController = player.GetComponent<Player.Controller>();
                Debug.Log(pController.m_myStackController.m_playersInStack);
                if (pController.m_myStackController.m_playersInStack > 1)
                {
                    Debug.Log("Found Stacked Players");
                    for(int pIdx = pController.m_myStackController.m_playersInStack - 1; pIdx > 0; pIdx--)
                    {
                        pController.m_myStackController.RemoveFromStack(pIdx);
                    }

                }
            }


            SceneController.CallTransitionToScene(m_sceneName);
        }
    }

}
