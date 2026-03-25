using UnityEngine;

public class SuckInPharaoh : MonoBehaviour
{
    public bool m_isSarcophagasClosed = false;

    [Header("References")]
    public GameObject m_pharaoh; //this must be set to the pharaoh as a whole
    public Transform m_pharaohTransform; //this must references the 'Enemy' child
    public GameObject m_tornadoPrefab;
    private GlyphSlots m_glyphSlots;
    private Animator m_animator;

    private void Awake()
    {
        m_glyphSlots = GetComponent<GlyphSlots>();
        m_animator = GetComponent<Animator>();       
    }

    private void Update()
    {
        if (m_glyphSlots.m_allGlyphSlotsFull == true)
        {
            m_animator.enabled = true;
            //HandleSuckInPharaoh();
        }
    }

    private void HandleSuckInPharaoh()
    {
        if (!m_isSarcophagasClosed)
        {
            GameObject tornadoGO = Instantiate(m_tornadoPrefab, m_pharaohTransform.position, Quaternion.identity);
            m_pharaoh.SetActive(false);

            //move tornado to sarc

            if (tornadoGO.transform.position == transform.position)
            {
                Destroy(tornadoGO);
                m_animator.enabled = true;
                m_isSarcophagasClosed = true;
            }
        }
    }
}
