using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Game_Class_Library {

    /* 
     * Used to create and access instances of the hand class implimented within both games. 
     * Used for quantities of the card class.
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public class CardPile {

        public const int NUM_SUITS = 4;
        public const int NUM_CARDS_PER_SUIT = 13;

        private List<Card> pile;

        /* creates an empty pile (no cards in the pile)
         * precondition: true
         * postcondition: true
         */
        public CardPile() {
            this.pile = new List<Card> { };
        }


        /* Used to generate piles of either a blank pile or unique cards in a pile.
         * precondition: requires either true or false 
         * postcondition: If true, generates 52 distinct cards. If false, generates empty pile.
         */     
        public CardPile(Boolean pile) {
            if (pile == true) {
                this.pile = new List<Card> { };
                foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                    if (suit == Suit.Diamonds || suit == Suit.Hearts) {
                        foreach (FaceValue value in Enum.GetValues(typeof(FaceValue))) {
                            this.pile.Add(new Card(suit, value, Colour.Red));
                        }
                    } else {
                        foreach (FaceValue value in Enum.GetValues(typeof(FaceValue))) {
                            this.pile.Add(new Card(suit, value, Colour.Black));
                        }
                    }
                }
            } else {
                this.pile = new List<Card> { };
            }
        }


        /* Used to add a card to the pile.
         * precondition: requires a valid card which is being added to the pile
         * postcondition: The card is then put into the required pile.
         */
        public void AddCard(Card card) {
            pile.Add(card);
        }


        /* Retrieves the number of cards in the pile.
         * precondition: true
         * postcondition: Returns an integer of the number of cards in the pile.
         */
        public int GetCount() {
            return pile.Count();
        }


        /* Retrieves the last card in the pile.
         * precondition: true
         * postcondition: Returns the card  in the last position of the pile, but does not remove it from the pile
         */
        public Card GetLastCardInPile() {
            int length = GetCount() - 1;
            return pile[length];
        }


        /* Used to shuffle a pile
         * precondition: true
         * postcondition: shuffles the pile of cards
         */
        public void ShufflePile() {
            //pile = pile.OrderBy(Suit => random.Next());
            Random random = new Random();
            int n = pile.Count;
            while (n > 1) {
                n--;
                int next = random.Next(n + 1);
                Card value = pile[next];
                pile[next] = pile[n];
                pile[n] = value;
            }

        }


        /* Used to deal a card into a card pile
         * precondition: true
         * postcondition: removes the card from the pile and returns the card
         */
        public Card DealOneCard() {
            Card deltCard = GetLastCardInPile();
            pile.RemoveAt(pile.Count - 1);
            return deltCard;
        }


        /* Used to deal cards into a card pile
         * precondition: requires a valid int value which is less than the length of the list
         * postcondition: removes the cards from the pile and returns the cards
         */
        public List<Card> DealCards(int numberOfCards) {
            List<Card> cardHand = new List<Card> { };

            for (int i = 0; i < numberOfCards; i++) {
                cardHand.Add(pile[i]);
                pile.RemoveAt(i);
            }

            return cardHand;
        }


        /* Removes last card in pile
         * precondition: true
         * postcondition: remove the last card in the card pile
         */
        public void RemoveLastCard() {
            int length = GetCount() - 1;
            pile.RemoveAt(length);
        }

    }
}
