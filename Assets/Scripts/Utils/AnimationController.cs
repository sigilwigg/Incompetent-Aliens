using UnityEngine;

/*
 *  Helpful static methods for use in changing aniamtion states in an animator.
 *  Helps to avoid messy, crazy animation graphs.
 *  
 *  ChangeAnimationState()  =>  change aniamtion state using crossfade. Will not interupt its own animation.
 *                              returns the new state.
 *  ForceTransitionTo()     =>  Forces an abrupt change to a new animation
 *                              does not return new state, does not take in current state.
 */

public class AnimationController : MonoBehaviour
{
    public static string ChangeAnimationState(Animator animator, string currentState, string newState, float speed = 0.2f, int layer = 0)
    {
        // ----- stop animation from interrupting itself -----
        if (currentState == newState) return currentState;
        currentState = newState;

        // ----- make state change -----
        animator.CrossFadeInFixedTime(currentState, speed, layer);
        return currentState;
    }

    public static void ForceTransitionTo(Animator animator, string newAnimation, float speed = 0.2f, int layer = 0)
    {
        // ----- make state change -----
        animator.CrossFadeInFixedTime(newAnimation, speed, layer);
    }
}
