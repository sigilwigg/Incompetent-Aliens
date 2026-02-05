using UnityEngine;

public class StackAnimController : MonoBehaviour
{
    public float m_avgTopForce;
    public float m_avgBotForce;

    Animator m_animator;
    public string m_currentAnimationState;
    public string FORWARD = "Forward";
    public string BACKWARD = "Backward";

    private void Start()
    {
        
    }
}
