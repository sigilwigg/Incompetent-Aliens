using UnityEngine;

public class SuckInPharaoh : MonoBehaviour
{
    public GameObject m_pharaoh; //this must be set to the pharaoh as a whole
    public Transform m_pharaohTransform; //this must references the 'Enemy' child
    public GameObject m_tornadoPrefab;
    public GlyphSlots m_glyphSlots;
    public bool m_isSarcophagasClosed = false;

    private void Start()
    {
        m_glyphSlots = GetComponent<GlyphSlots>();
    }

    private void Update()
    {
        if (!m_isSarcophagasClosed)
        {
            HandleSuckInPharaoh();
        }
    }

    private void HandleSuckInPharaoh()
    {
        if (m_glyphSlots.m_allGlyphSlotsFull == true)
        {
            GameObject tornadoGO = Instantiate(m_tornadoPrefab, m_pharaohTransform.position, Quaternion.identity);
            m_pharaoh.SetActive(false);

            //move tornado to sarc

            if (tornadoGO.transform.position == transform.position)
            {
                Destroy(tornadoGO);
                //play closing lid animation
                m_isSarcophagasClosed = true;
            }
        }
    }
}
