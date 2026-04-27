using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class LevelData : MonoBehaviour
{
    public int m_tierA;
    public int m_tierB;
    public int m_tierC;
    public int m_tierD;
    public int m_tierE;
    public int m_tierF;

    public string EvaluateGrade(int _ms)
    {
        string result = "F";

        if (_ms <= m_tierE) result = "E";
        if (_ms <= m_tierD) result = "D";
        if (_ms <= m_tierC) result = "C";
        if (_ms <= m_tierB) result = "B";
        if (_ms <= m_tierA) result = "A";

        return result;
    }
}
