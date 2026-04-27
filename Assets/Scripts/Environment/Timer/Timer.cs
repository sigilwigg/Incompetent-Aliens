using TMPro;
using UnityEngine;

/*
 *  Basic timer for reference by other classes and for displaying time count on the ui.
 *  Can be adjusted to be a conut-down instead if desired.
 *  
 *  UpdateTimer()       =>  Update based on deltaTime
 *  SetTimerText()      =>  Update the text readout for the timer in the UI
 *  RestartTimer()      =>  Public function that can be called from scripts to restart/start the timer.
 */

public class Timer : MonoBehaviour
{
    public TMP_Text m_TextMeshPro;
    public float m_timeElapsed = 0.0f;
    private bool m_isStopped = false;

    private void Start()
    {
        m_timeElapsed = 0.0f;
    }

    private void Update()
    {
        if(!m_isStopped) UpdateTimer();
        SetTimerText();
    }

    private void UpdateTimer()
    {
        m_timeElapsed += Time.deltaTime;
    }

    private void SetTimerText()
    {
        float ms = m_timeElapsed * 1000;
        int hours = Mathf.FloorToInt(ms / 3600000);
        ms %= 3600000;

        int minutes = Mathf.FloorToInt(ms / 60000);
        ms %= 60000;

        int seconds = Mathf.FloorToInt(ms / 1000);

        float timeForText = Mathf.Floor(m_timeElapsed);
        m_TextMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int GetMS()
    {
        return Mathf.FloorToInt((m_timeElapsed * 1000));
    }

    public void RestartTimer()
    {
        m_timeElapsed = 0.0f;
    }

    public void PauseTimer()
    {
        m_isStopped = true;
    }

    public void UnpauseTimer()
    {
        m_isStopped = false;
    }
}