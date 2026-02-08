using UnityEngine;
using Player;
using System.Collections.Generic;

/*
* This script is to let the player place the glyph inside the slot
* 
* To use this, add the script to the base of the Sarcophagus
* Set Glyph 1 as the first glyph, and Slot 1 as the place you want it to get inserted
* Do the same with Glyph 2 and Slot 2
*/



public class GlyphSlots : MonoBehaviour
{
    private Controller m_playerController;

    // ----- These will allow the developers to add a specific item of their request to each of the variables -----
    [Header("Glyph Attributes")]
    public List<GameObject> m_glyphs = new List<GameObject>();
    public List<GameObject> m_slots = new List<GameObject>();
    public List<bool> m_isGlyphInPlace = new List<bool>();

    [Header("Alien Attributes")]
    public float reach = 1f;

    private void Awake()
    {
        m_playerController = GameObject.FindWithTag("Player").GetComponent<Controller>();
    }

    private void Update()
    {
        for (int idx = 0; idx < m_glyphs.Count; idx++)
        {
            HandleGlyphPlacement(idx);
        }
    }

    // ----- Get the distance to the slot -----
    private void HandleGlyphPlacement(int glyphIndex)
    {
        // ----- These variables determine the range at which you can put the glyph in -----
        float distance;

        // ----- This calculates the distance between the glyph and slot, and outputs it as a float -----
        distance = Vector3.Distance(m_glyphs[glyphIndex].transform.position, m_slots[glyphIndex].transform.position);

        if (reach > distance && !m_isGlyphInPlace[glyphIndex])
        {
            m_isGlyphInPlace[glyphIndex] = true;

            m_glyphs[glyphIndex].transform.position = m_slots[glyphIndex].transform.position;

            m_playerController.DropItem();
        }
    }
}
