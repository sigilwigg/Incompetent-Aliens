using UnityEngine;

public class SoundtrackTrigger : MonoBehaviour
{
    public string m_areaSoundtrackName;

    private void Start()
    {
        AudioManager.instance.ChangeMusic(m_areaSoundtrackName);
    }
}
