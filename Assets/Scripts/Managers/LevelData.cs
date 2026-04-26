using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class LevelData : MonoBehaviour
{
    public List<Tuple<string, int>> m_gradingTiers = new List<Tuple<string, int>>();

    public int m_tierA;
    public int m_tierB;
    public int m_tierC;
    public int m_tierD;
    public int m_tierE;
    public int m_tierF;

    //public string EvaluateGrade(int _ms)
    //{
    //    string result = m_gradingTiers.ElementAt(m_tierF).Key;

    //    if (_ms <= m_gradingTiers.ElementAt(m_tierE).Value) result = m_gradingTiers.ElementAt(m_tierE).Key;
    //    if (_ms <= m_gradingTiers.ElementAt(m_tierD).Value) result = m_gradingTiers.ElementAt(m_tierD).Key;
    //    if (_ms <= m_gradingTiers.ElementAt(m_tierC).Value) result = m_gradingTiers.ElementAt(m_tierC).Key;
    //    if (_ms <= m_gradingTiers.ElementAt(m_tierB).Value) result = m_gradingTiers.ElementAt(m_tierB).Key;
    //    if (_ms <= m_gradingTiers.ElementAt(m_tierA).Value) result = m_gradingTiers.ElementAt(m_tierA).Key;

    //    return result;
    //}
}
