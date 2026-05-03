using System.Collections;
using UnityEngine;

public class SuckInPharaoh : MonoBehaviour
{
    public bool m_isSarcophagasClosed = false;

    [Header("References")]
    [Tooltip("This must be set to the GameObject called 'Pharaoh'")]
    public GameObject m_pharaoh;
    [Tooltip("This must reference the 'Enemy' child of the GameObject 'Pharaoh'")]
    public Transform m_pharaohTransform;
    public GameObject m_tornadoPrefab;
    public GameObject m_trappedPharaoh;

    private GlyphSlots m_glyphSlots;
    private Animator m_animator;
    private GameObject m_activeTornado; // Tracks the single active tornado

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

        if(m_isSarcophagasClosed)
        {
            GameState.instance.m_isLevelComplete = true;
        }
    }

    private void HandleSuckInPharaoh()
    {
        if(m_isSarcophagasClosed) return;

        // ----- If a tornado is already active, do not spawn another ----
        if(m_activeTornado != null) return;

        // ----- Spawn single tornado at the pharaoh's position and hide pharaoh -----
        m_activeTornado = Instantiate(m_tornadoPrefab, m_pharaohTransform.position, Quaternion.identity);
        m_pharaoh.SetActive(false);

        StartCoroutine(MoveTornadoToSarcophagus(m_activeTornado));
    }

    private IEnumerator MoveTornadoToSarcophagus(GameObject tornadoGO)
    {
        if(tornadoGO == null) yield break;

        Transform tornadoTransform = tornadoGO.transform;
        Vector3 sarcophagusPosition = transform.position;

        // ----- Lock Y to tornado's current Y to ensure horizontal movement only ----
        Vector3 targetXZ = new Vector3(sarcophagusPosition.x, tornadoTransform.position.y, sarcophagusPosition.z);

        // ----- Move tornado towards targetXZ until within arrival threshold ----
        while(Vector3.Distance(new Vector3(tornadoTransform.position.x, 0f, tornadoTransform.position.z), new Vector3(targetXZ.x, 0f, targetXZ.z)) > m_tornadoArrivalThreshold)
        {
            Vector3 next = Vector3.MoveTowards(tornadoTransform.position, targetXZ, m_tornadoMoveSpeed * Time.deltaTime);
            next.y = tornadoTransform.position.y;
            tornadoTransform.position = next;
            yield return null;
        }

        // ---- Finalize -----
        Destroy(tornadoGO);
        m_trappedPharaoh.SetActive(true);
        m_activeTornado = null;
        m_animator.enabled = true;
        m_isSarcophagasClosed = true;
    }
}
