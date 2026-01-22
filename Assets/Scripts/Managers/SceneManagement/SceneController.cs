using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Scene Controller Package - Wyatt H Williams
 * 
 * Call the following from anywhere to trigger transition specifying scene name
 *      SceneController.CallTransitionToScene("MyScene");
 *      
 * SceneLoader prefab contains necessary animator stuffs.
 * SceneLoader lives in the SceneManagement namespace.
 * SceneController is a public class with public static methods only.
 * 
 * Place SceneLoader prefab in each scene to function.
 * 
 * For different kinds of transitions, create more animations and assign them
 * to an animation override. Then assign the override to the animator.
 * Be sure that the new animations have "loop time" set to false.
 * 
 * The additive methods were only added to keep logic cetnralized.
 * Instead of splitting control between SceneController and SceneManager, we'll use SceneController
 * so we don't get confused.
 */

public class SceneController : MonoBehaviour
{
    public static void CallTransitionToScene(string sceneName)
    {
        SceneManagement.SceneLoader sceneLoader =
            GameObject.FindWithTag("SceneLoader").GetComponent<SceneManagement.SceneLoader>();

        sceneLoader.TransitionToScene(sceneName);
    }

    public static void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public static void UnloadSceneAdditive(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
