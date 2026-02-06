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

namespace Timer
{
    public class Timer : MonoBehaviour
    {
        public TMP_Text m_TextMeshPro;
        public float m_timeElapsed = 0.0f;

        private void Start()
        {
            m_timeElapsed = 0.0f;
        }

        private void Update()
        {
            UpdateTimer();
            SetTimerText();
        }

        private void UpdateTimer()
        {
            m_timeElapsed += Time.deltaTime;
        }

        private void SetTimerText()
        {
            float timeForText = Mathf.Floor(m_timeElapsed);
            m_TextMeshPro.text = timeForText.ToString();
        }

        public void RestartTimer()
        {
            m_timeElapsed = 0.0f;
        }
    }
}
