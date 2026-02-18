using Enemy;
using Enemy.Pharaoh;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Observation))]
public class AIObserveEditor : Editor
{

    private void OnSceneGUI()
    {
        // ----- Draw vision range editor -----
        AIObserve observe = (AIObserve)target;
        Handles.color = Color.aliceBlue;
        Handles.DrawWireArc(observe.transform.position, Vector3.up, Vector3.forward, 360, observe.m_visionRange);

        // ----- Draw vision angle lines in the editor -----
        Vector3 visionAngleLeft = DirFromAngle(observe.transform.eulerAngles.y, -observe.m_visionAngle / 2);
        Vector3 visionAngleRight = DirFromAngle(observe.transform.eulerAngles.y, observe.m_visionAngle / 2);
        Handles.color = Color.red;
        Handles.DrawLine(observe.transform.position, observe.transform.position + visionAngleLeft * observe.m_visionRange);
        Handles.DrawLine(observe.transform.position, observe.transform.position + visionAngleRight * observe.m_visionRange);
    }

    private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
