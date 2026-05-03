using UnityEngine;

/*
 *  Base class for all Interactables
 */

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
       public string m_interactableText;
       public int m_interactableRadius;

        public virtual void Interact(Player.Controller playerController)
        {
            
        }
    }

}
