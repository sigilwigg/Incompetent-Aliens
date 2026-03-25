using UnityEngine;
using Player;
using System.Collections.Generic;
using Interactables;
using System.Linq;

/*
* This script is to let the player place the glyph inside the slot
* 
* To use this, add the script to the base of the Sarcophagus
* Set Glyph 1 as the first glyph, and Slot 1 as the place you want it to get inserted
* Do the same with Glyph 2 and Slot 2
*/



public class GlyphSlots : MonoBehaviour
{
    private Controller m_playerToDrop;

    // ----- These will allow the developers to add a specific item of their request to each of the variables -----
    [Header("Glyph Attributes")]
    public bool m_allGlyphSlotsFull = false;
    public List<GameObject> m_glyphs = new List<GameObject>();
    public List<GameObject> m_slots = new List<GameObject>();
    public List<bool> m_isGlyphInPlace = new List<bool>();

    [Header("Alien Attributes")]
    public float m_playerReach = 1f;

    private void Update()
    {
        for (int idx = 0; idx < m_glyphs.Count; idx++)
        {
            HandleGlyphPlacement(idx);
        }

        //check if all glyphs are true
        m_allGlyphSlotsFull = m_isGlyphInPlace.All(b  => b);
    }

    // ----- Get the distance to the slot -----
    private void HandleGlyphPlacement(int glyphIndex)
    {
        // ----- This calculates the currentPlayerDistanceFromGlyph between the glyph and slot, and outputs it as a float -----
        float currentPlayerDistanceFromGlyph = Vector3.Distance(m_glyphs[glyphIndex].transform.position, m_slots[glyphIndex].transform.position);

        if (m_playerReach > currentPlayerDistanceFromGlyph && !m_isGlyphInPlace[glyphIndex])
        {
            // modify glyph placement
            m_isGlyphInPlace[glyphIndex] = true;
            m_glyphs[glyphIndex].SetActive(false);
            m_slots[glyphIndex].SetActive(true);

            // update player
            m_playerToDrop = m_glyphs[glyphIndex].GetComponent<Pickupable>().m_playerController;
            m_playerToDrop.DropItem();
        }
    }


}
