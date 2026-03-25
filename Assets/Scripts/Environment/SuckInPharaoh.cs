using System.Collections;
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

    [Header("Tornado Movement")]
    public float m_tornadoMoveSpeed = 5f;
    public float m_tornadoArrivalThreshold = 0.05f;

    private void Awake()
    {
        m_glyphSlots = GetComponent<GlyphSlots>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(m_glyphSlots.m_allGlyphSlotsFull == true)
        {
            HandleSuckInPharaoh();
        }
    }

    private void HandleSuckInPharaoh()
    {
        if(m_isSarcophagasClosed)
            return;

        // ----- Spawn tornado at the pharaohs position and hide pharaoh -----
        GameObject tornadoGO = Instantiate(m_tornadoPrefab, m_pharaohTransform.position, Quaternion.identity);
        m_pharaoh.SetActive(false);

        StartCoroutine(MoveTornadoToSarcophagus(tornadoGO));
    }

    private IEnumerator MoveTornadoToSarcophagus(GameObject tornadoGO)
    {
        Transform tornadoTransform = tornadoGO.transform;
        Vector3 target = transform.position;

        // ----- Move until within arrival threshold or tornado is destroyed -----
        while(Vector3.Distance(tornadoTransform.position, target) > m_tornadoArrivalThreshold)
        {
            tornadoTransform.position = Vector3.MoveTowards(tornadoTransform.position, target, m_tornadoMoveSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(tornadoGO);
        m_animator.enabled = true;
        m_isSarcophagasClosed = true;
    }
}
