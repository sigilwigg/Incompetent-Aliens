using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Scene Controller Package - Wyatt H Williams
 * 
 * Call the following from anywhere to trigger transition specifying scene name
 *      s_SceneController.CallTransitionToScene("MyScene");
 *      
 * SceneLoader prefab contains necessary animator stuffs.
 * SceneLoader lives in the SceneManagement namespace.
 * s_SceneController is a public class with public static methods only.
 * 
 * Place SceneLoader prefab in each scene to function.
 */

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public Animator m_transitionAnimator;
        public float m_transitionTime = 1f;

        public void TransitionToScene(string sceneName)
        {
            StartCoroutine(Transition(sceneName, m_transitionTime));
        }

        IEnumerator Transition(
            string sceneName, 
            float secondsToWait
            )
        {
            // ----- play animation -----
            m_transitionAnimator.SetTrigger("Start");

            // ----- wait -----
            yield return new WaitForSeconds(secondsToWait);

            // ----- load next scene -----
            SceneManager.LoadScene(sceneName);
        }
    }
}