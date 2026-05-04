using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackLean : MonoBehaviour
{
    public enum State
    {
        Wait,
        Idle,
        PickFallDirection,
        FallInDirection,
        FallApart
    }
    public State m_currentState;

    public Stack.Controller m_stackController;

    private int  m_fallDirectionIdx = 0;
    public float m_fallTimeTotal = 2.0f;
    public float m_correctionTimeMultiplier = 1.5f;
    public float m_waitTime = 3.0f;

    public List<Transform> m_stackPositions = new List<Transform>();
    public List<float> m_targetLeanPercentages = new List<float>();
    public List<Vector2> m_fallDirections = new List<Vector2>();
    public float m_fallDistance = 2.0f;
    public List<Vector2> m_fallTargets = new List<Vector2>();

    private IEnumerator m_currentCoroutine;
    public AnimationCurve m_moveCurve;

    public bool m_isCounteracted = false;

    public GameObject m_bouncingBallPrefab;
    public float m_bounceForce = 100.0f;

    private void Start()
    {
        // ----- set fall targets at direction * distance;
        m_fallTargets.Clear();
        foreach(Vector2 direction in m_fallDirections)
        {
            m_fallTargets.Add(direction * m_fallDistance);
        }

        ChangeState(State.Idle);
    }

    private void Update()
    {
        float leaningRotation = Mathf.Atan2(m_fallDirections[m_fallDirectionIdx].x, m_fallDirections[m_fallDirectionIdx].y);
        leaningRotation *= Mathf.Rad2Deg;
        float playerRotation = m_stackController.m_playerController.m_rotation;
        bool isPlayerMoving = m_stackController.m_playerController.m_moveInput == Vector2.zero ? false : true;

        m_isCounteracted = false;
        for(int idx = 1; idx < m_stackController.m_playerControllers.Count; idx++)
        {
            if (m_stackController.m_playerControllers[idx] == null) break;
            float rotation = Mathf.Atan2(
                m_stackController.m_playerControllers[idx].m_moveInput.x,
                m_stackController.m_playerControllers[idx].m_moveInput.y
            );
            rotation *= Mathf.Rad2Deg;

            // ----- and now for a true pro programmnig moment -----
            if      (leaningRotation == 0   && rotation == 180)     m_isCounteracted = true;
            else if (leaningRotation == 90  && rotation == -90)     m_isCounteracted = true;
            else if (leaningRotation == -90 && rotation == 90)      m_isCounteracted = true;
            else if (leaningRotation == 180 && rotation == 0)       m_isCounteracted = true;
            else if (leaningRotation == 45  && rotation == -135)    m_isCounteracted = true;
            else if (leaningRotation == -45 && rotation == 135)     m_isCounteracted = true;
            else if (leaningRotation == 135 && rotation == -45)     m_isCounteracted = true;
            else if (leaningRotation == -135&& rotation == 45)      m_isCounteracted = true;

            if (m_isCounteracted) break;
        }

        if(!m_isCounteracted)
            if (playerRotation == leaningRotation && isPlayerMoving) m_isCounteracted = true;
            else m_isCounteracted = false;
    }

    public void RestartLean()
    {
        ChangeState(State.Idle);
    }

    void ChangeState(State nextState)
    {
        if (m_currentState == nextState) return;
        Exit(m_currentState);
        m_currentState = nextState;
        Enter(m_currentState);
    }

    void Exit(State state)
    {
        if (m_currentCoroutine != null) StopCoroutine(m_currentCoroutine);
        m_currentCoroutine = null;
    }

    void Enter(State state)
    {
        switch (state)
        {
            case State.Wait:
                m_currentCoroutine = Wait();
                break;
            case State.Idle:
                m_currentCoroutine = Idle();
                break;
            case State.PickFallDirection:
                m_currentCoroutine = PickFallDirection();
                break;
            case State.FallInDirection:
                m_currentCoroutine = FallInDirection();
                break;
            case State.FallApart:
                m_currentCoroutine = FallApart();
                break;
            default:
                Debug.Log("state for enter not found");
                break;
        }

        StartCoroutine(m_currentCoroutine);
    }

    IEnumerator PickFallDirection()
    {
        m_fallDirectionIdx = Random.Range(0, m_fallDirections.Count - 1);

        ChangeState(State.FallInDirection);
        yield return null;
    }

    IEnumerator FallInDirection()
    {
        float timeElapsed = 0.0f;
        List<Vector3> startingPositions = new List<Vector3>();
        List<Vector3> targetPositions = new List<Vector3>();

        // ----- construct start and end poisitions -----
        
        for (int idx = 0; idx < m_stackPositions.Count; idx++)
        {
            // starting position
            Vector3 startPosition = new Vector3(0, m_stackPositions[idx].localPosition.y, 0);
            startingPositions.Add(startPosition);

            // target position
            Vector3 targetPosition = new Vector3(m_fallTargets[m_fallDirectionIdx].x, m_stackPositions[idx].localPosition.y, m_fallTargets[m_fallDirectionIdx].y);
            targetPosition = Vector3.Lerp(startPosition, targetPosition, m_targetLeanPercentages[idx]);
            targetPositions.Add(targetPosition);
        }

        while (timeElapsed >= 0.0f)
        {
            timeElapsed = timeElapsed > m_fallTimeTotal ? m_fallTimeTotal : timeElapsed;
            float sample = timeElapsed / m_fallTimeTotal;
            sample = m_moveCurve.Evaluate(sample);

            for (int idx = 0; idx < m_stackPositions.Count; idx++)
            {
                m_stackPositions[idx].localPosition = Vector3.Lerp(startingPositions[idx], targetPositions[idx], sample);
            }

            if (!m_isCounteracted)
                // fall outward
                timeElapsed += TimeManager.instance.deltaTime;
            else
                // balance inward
                timeElapsed -= (TimeManager.instance.deltaTime * m_correctionTimeMultiplier);

            yield return null;
        }

        ChangeState(State.Idle);
        yield return null;
    }

    IEnumerator FallApart()
    {
        if (m_stackController.m_playersInStack > 1)
        {
            for (int idx = 1; idx < m_stackController.m_playersInStack; idx++)
            {
                GameObject bouncePositioner = GameObject.Instantiate(m_bouncingBallPrefab, m_stackPositions[idx].position, Quaternion.identity);
                BouncyBall bouncyBall = bouncePositioner.GetComponent<BouncyBall>();
                bouncyBall.SetPropelSelf(
                    new Vector3(m_fallDirections[m_fallDirectionIdx].x, 0.0f, m_fallDirections[m_fallDirectionIdx].y),
                    m_bounceForce
                );

                Player.Controller playerController = m_stackController.m_playerControllers[idx];
                m_stackController.RemoveFromStack(idx);
                playerController.SetBeingThrown(bouncePositioner.transform);
            }
        }

        ChangeState(State.Wait);
        yield return new WaitForFixedUpdate();
    }

    IEnumerator Wait()
    {
        float timeElapsed = 0.0f;

        while (timeElapsed < m_waitTime)
        {
            timeElapsed += TimeManager.instance.deltaTime;
            yield return null;
        }

        ChangeState(State.Idle);
        yield return null;
    }

    IEnumerator Idle()
    {
        foreach (Transform stackPosition in m_stackPositions)
        {
            Vector3 targetPosition = new Vector3(
                0,
                stackPosition.localPosition.y,
                0
            );

            stackPosition.localPosition = targetPosition;
        }

        ChangeState(State.PickFallDirection);
        yield return null;
    }
}
