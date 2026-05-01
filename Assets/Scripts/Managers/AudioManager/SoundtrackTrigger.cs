using UnityEngine;

public class SoundtrackTrigger : MonoBehaviour
{
    public string m_areaSoundtrackName;

    private void Start()
    {
        if (m_areaSoundtrackName != AudioManager.instance.currentMusicId)
        {
            Debug.Log("change music");
            AudioManager.instance.ChangeMusic(m_areaSoundtrackName);
        }
           
    }
}
