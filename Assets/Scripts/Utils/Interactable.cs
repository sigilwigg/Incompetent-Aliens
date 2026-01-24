using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string m_interactableText;
    public int m_interactableRadius;

    public void Interact()
    {
        Debug.Log(m_interactableText);
    }
}
