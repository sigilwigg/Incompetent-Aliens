using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteGrade : MonoBehaviour
{
    public List<GameObject> m_grades = new List<GameObject>();

    void Start()
    {
        foreach (GameObject grade in m_grades)
        {
            grade.SetActive(false);
        }

        if (GameState.instance == null)
        {
            m_grades[0].SetActive(true);
            return;
        }

        if (GameState.instance.m_recordedLevelGrade == "A") m_grades[0].SetActive(true);
        if (GameState.instance.m_recordedLevelGrade == "B") m_grades[1].SetActive(true);
        if (GameState.instance.m_recordedLevelGrade == "C") m_grades[2].SetActive(true);
        if (GameState.instance.m_recordedLevelGrade == "D") m_grades[3].SetActive(true);
        if (GameState.instance.m_recordedLevelGrade == "E") m_grades[4].SetActive(true);
        if (GameState.instance.m_recordedLevelGrade == "F") m_grades[5].SetActive(true);
    }
}
