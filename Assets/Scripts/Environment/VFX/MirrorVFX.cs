using UnityEngine;

/* 
 * This script is added to the Mirror prefab.
 * You will need to add the Empty Mirror Glow prefab to the level, and place it where the "MirrorZone" is within the Pharoah prefab.
 * Drag the Mirror Outline from the Empty Mirror Glow prefab into the Mirror Outline in the inspector
*/

public class MirrorVFX : MonoBehaviour
{
    public ParticleSystem mirrorOutline;
    public GameObject mirror;

    void Update()
    {
        CheckPickup();
    }
    private void CheckPickup()
    {
        if (!mirror.GetComponent<MirrorInMirrorZone>().m_isMirrorInPlace)
        {
            mirrorOutline.Play();
        }
        else
        {
            mirrorOutline.Stop();
        }
    }
}
