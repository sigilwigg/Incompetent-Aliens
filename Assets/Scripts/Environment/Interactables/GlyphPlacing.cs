using UnityEngine;
using Player;
using Interactables;
using TMPro;



/*
 * This script is to let the player place the glyph inside the slot
 * 
 * To use this, add the script to the base of the Sarcophagus
 * Set Glyph 1 as the first glyph, and Slot 1 as the place you want it to get inserted
 * Do the same with Glyph 2 and Slot 2
 */

namespace Glyph
{

    public class GlyphPlacing : MonoBehaviour
    {
        // ----- These will allow the developers to add a specific item of their request to each of the variables -----
        [Header("First Glyph")]
        public GameObject m_glyph1;
        public GameObject m_slot1;

        [Header("Second Glyph")]
        public GameObject m_glyph2;
        public GameObject m_slot2;

        [Header("In Place Checker")]
        public bool m_glyph1InSlot = false;
        public bool m_glyph2InSlot = false;

        public float reach = 1f;

        private Controller m_playerController;

        private void Update()
        {
            Glyph1();
            Glyph2();
        }

        // ----- Get the distance to the slot -----
        private void Glyph1()
        {

            // ----- These variables determine the range at which you can put the glyph in -----
            float distance;

            // ----- This calculates the distance between the glyph and slot, and outputs it as a float -----
            distance = Vector3.Distance(m_glyph1.transform.position, m_slot1.transform.position);

            if (reach > distance)
            {
                m_glyph1InSlot = true;

                m_glyph1.transform.position = m_slot1.transform.position;

                // Pickupable itemToDrop = m_playerController.m_currentlyHeldItem.GetComponent<Interactables.Pickupable>();

                // if (itemToDrop != null) itemToDrop.m_collider.enabled = true;
                // itemToDrop.gameObject.transform.parent = null;
                // m_playerController.m_currentlyHeldItem = null;
            }
            else
            {
                m_glyph1InSlot = false;
            }
        }

        private void Glyph2()
        {
            float distance;

            distance = Vector3.Distance(m_glyph2.transform.position, m_slot2.transform.position);

            if (reach > distance)
            {
                m_glyph2InSlot = true;

                m_glyph2.transform.position = m_slot2.transform.position;
            }
            else
            {
                m_glyph2InSlot = false;
            }
        }


    }
}