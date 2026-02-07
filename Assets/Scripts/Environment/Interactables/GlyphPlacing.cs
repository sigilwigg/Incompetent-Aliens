using UnityEngine;
using Player;
using Interactables;

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

        [Header("Alien Attributes")]
        public float reach = 1f;

        private bool hasDone1 = false;
        private bool hasDone2 = false;

        private Controller m_playerController;

        private void Awake()
        {
            m_playerController = GameObject.FindWithTag("Player").GetComponent<Controller>();
        }

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

            if (reach > distance && !hasDone1)
            {
                m_glyph1InSlot = true;

                m_glyph1.transform.position = m_slot1.transform.position;

                m_playerController.DropItem();

                hasDone1 = true;
            }
        }

        private void Glyph2()
        {
            float distance;

            distance = Vector3.Distance(m_glyph2.transform.position, m_slot2.transform.position);

            if (reach > distance && !hasDone2)
            {
                m_glyph2InSlot = true;

                m_glyph2.transform.position = m_slot2.transform.position;

                m_playerController.DropItem();

                hasDone2 = true;
            }
        }
    }
}