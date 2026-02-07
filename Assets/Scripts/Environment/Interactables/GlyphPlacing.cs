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
        public GameObject Glyph_1;
        public GameObject Slot_1;

        public GameObject Glyph_2;
        public GameObject Slot_2;

        public bool Glyph_1_inPlace = false;
        public bool Glyph_2_inPlace = false;

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
            float reach = 1.5f;
            float distance;

            // ----- This calculates the distance between the glyph and slot, and outputs it as a float -----
            distance = Vector3.Distance(Glyph_1.transform.position, Slot_1.transform.position);

            if (reach > distance)
            {
                Glyph_1_inPlace = true;

                Glyph_1.transform.position = Slot_1.transform.position;

                // Pickupable itemToDrop = m_playerController.m_currentlyHeldItem.GetComponent<Interactables.Pickupable>();

                // if (itemToDrop != null) itemToDrop.m_collider.enabled = true;
                // itemToDrop.gameObject.transform.parent = null;
                // m_playerController.m_currentlyHeldItem = null;
            }
            else
            {
                Glyph_1_inPlace = false;
            }
        }

        private void Glyph2()
        {
            float reach = 1.5f;
            float distance;

            distance = Vector3.Distance(Glyph_2.transform.position, Slot_2.transform.position);

            if (reach > distance)
            {
                Glyph_2_inPlace = true;

                Glyph_2.transform.position = Slot_2.transform.position;
            }
            else
            {
                Glyph_2_inPlace = false;
            }
        }


    }
}