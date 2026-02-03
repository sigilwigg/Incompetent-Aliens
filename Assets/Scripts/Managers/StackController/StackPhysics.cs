using UnityEngine;

public class StackPhysics : MonoBehaviour
{
    private Player.Stack m_playerStack;
    public Transform[] m_stackRigPositions;
    public Transform[] m_stackFinalPositions;
    public float[] m_stackHeights = new float[4];

    public float m_heightTop = 3.5f;
    public float m_heightThird = 2.5f;
    public float m_heightSecond = 1.5f;
    public float m_heightBottom = 0.5f;

    public bool m_thing = true;

    private void Start()
    {
        m_playerStack = GetComponent<Player.Stack>();
        m_stackHeights[0] = m_heightBottom;
        m_stackHeights[1] = m_heightSecond;
        m_stackHeights[2] = m_heightThird;
        m_stackHeights[3] = m_heightTop;
    }

    private void Update()
    {
        bool isUpperMoveStronger = m_thing;
        if (m_playerStack.m_stackHeight > 1)
        {
            float moveStength = 0.0f;
            for (int heightLevel = 0; heightLevel < m_playerStack.m_stackHeight; heightLevel++)
            {

            }

            float upperMoveStrength = 1.0f - Vector2.Distance(m_playerStack.m_playerControllers[m_playerStack.m_stackHeight].m_movementInput, Vector2.zero);
            float LowerMoveStrength = 1.0f - Vector2.Distance(m_playerStack.m_playerControllers[0].m_movementInput, Vector2.zero);
            isUpperMoveStronger = upperMoveStrength > LowerMoveStrength ? true : false;
        }

        int currentPosition = 0;

        foreach(Transform transform in m_stackRigPositions)
        {
            int stackPosition = currentPosition;
            float sign = 1.0f;

            if (isUpperMoveStronger) sign = -1.0f;

            m_stackFinalPositions[stackPosition].localPosition = new Vector3(
                transform.localPosition.x * sign,
                transform.localPosition.y,
                transform.localPosition.z * sign
            );

            m_stackFinalPositions[stackPosition].localRotation = Quaternion.Euler(
                transform.localRotation.eulerAngles.x * sign,
                transform.localRotation.eulerAngles.y * sign,
                transform.localRotation.eulerAngles.z * sign
            );

            currentPosition++;
            //if (currentPosition >= m_playerStack.m_stackHeight) break;
        }
    }
}
