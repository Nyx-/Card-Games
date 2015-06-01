using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Shared_Game_Class_Library {

    /* 
     * Used to create and access instances of the hand class implimented within both games. 
     * Used for quantities of the card class.
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public class Hand : IEnumerable {

        private List<Card> cards;


        /* Used to create a empty hand
         * precondition: true
         * postcondition: initializes an empty hand
         */
        public Hand() {
        }


        /* Creates a Hand which is a list of cards
         * precondition: requires a valid list containing card values
         * postcondition: creates and references to the cards in the 'hand'
         */
        public Hand(List<Card> card) {
            this.cards = card;
        }


        /* Gets the count of the number of cards in the hand
         * precondition: true
         * postcondition: an integer of the number of cards in the hand
         */
        public int GetCount() {
            return cards.Count();
        }


        /* Gets the card at the specified index
         * precondition: requires a valid int value
         * postcondition: returns card at specified position
         */
        public Card GetCard(int card) {
            return cards[card];
        }


        /* Adds a card to the hand object
         * precondition: requires a valid card which is being added to the hand object
         * postcondition: the card is added to the hand object
         */
        public void AddCard(Card card) {
            cards.Add(card);
        }


        /* Checks to see if the hand contains the specified card
         * precondition: requires a valid card which is being cross-matched
         * postcondition: returns true if card is in the hand
         */
        public bool ContainsCard(Card card) {   
            return cards.Contains(card);
        }


        /* Removes the specified card from the hand
         * precondition: requires a valid card which is contained in the hand
         * postcondition: removes card from the hand if it is in the hand
         */
        public bool RemoveCard(Card card) {
            return cards.Remove(card);
        }


        /* Removes the card at specified position from the hand
         * precondition: requires a valid integer index of the card which is being removed
         * postcondition: removes the card at specified position in the hand
         */
        public void RemoveCardAt(int card) {
            cards.RemoveAt(card);
        }


        /* Sorts the hand
         * precondition: true
         * postcondition: sorts the hand specified
         */
        public void SortHand() {
            cards.Sort();
        }


        /* Gets the Emumerator of the hand
         * precondition: true
         * postcondition: returns the enumerator
         */
        public IEnumerator GetEnumerator() {
            return cards.GetEnumerator();
        }

    }
}
