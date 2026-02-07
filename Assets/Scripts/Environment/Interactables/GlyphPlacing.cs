using UnityEngine;
using Player;
using Interactables;
using TMPro;



/*
 * This script is to let the player place the glyph inside the slot
 * 
 * To use this, add the script to any glyph item
 * Set the "Glyph" as the item that you want to pick up
 * Set the "Sarcophagus" as the slot you want to place the item in
 */

namespace Glyph
{

    public class GlyphPlacing : MonoBehaviour
    {   
        // ----- These will allow the developers to add a specific item of their request to each of the variables -----
        public GameObject Glyph;
        public GameObject Slot;

        private void Update()
        {
            CheckDistance();
        }

        // ----- Get the distance to the slot -----
        private void CheckDistance()
        {

            // ----- These variables determine the range at which you can put the glyph in -----
            float reach = 2;
            float distance;
            
            // ----- This variable is a boolean that gets updated when the glyph is in place -----
            bool inPlace;

            // ----- This calculates the distance between the glyph and slot, and outputs it as a float -----
            distance = Vector3.Distance(Glyph.transform.position, Slot.transform.position);

            if (reach > distance)
            {
                inPlace = true;

                Glyph.transform.position = Slot.transform.position;
            }
            else
            {
                inPlace = false;
            }
        }


            


    }
}

