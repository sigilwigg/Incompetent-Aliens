using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class EndCredits : MonoBehaviour
    {
        public void OnMainMenuButtonPressed()
        {
            SceneController.CallTransitionToScene("MainMenu");
        }
    }

}
