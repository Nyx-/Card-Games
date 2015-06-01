using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Game_Class_Library {
    
    public enum Suit { Clubs, Diamonds, Hearts, Spades }

    public enum FaceValue {
        Two, Three, Four, Five, Six, Seven, Eight, Nine,
        Ten, Jack, Queen, King, Ace
    }

    public enum Colour {
        Black, Red
    }

    /* 
     * Used to create and access instances of the card class implimented within both games.
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public class Card : IEquatable<Card>, IComparable<Card> {

        private FaceValue faceValue;
        private Suit suit;
        private Colour colour;


        /* Used to create a "blank" card
         * precondition: true
         * postcondition: initializes a "blank" card
         */
        public Card() {
        }


        /* Assigns the references of suit and face values
         * precondition: requires valid suit and faceValue values
         * postcondition: the card is attached to a suit and face value
         */
        public Card(Suit suit, FaceValue faceValue) {
            this.suit = suit;
            this.faceValue = faceValue;
        }


        /* Assigns the references of suit and face values
         * precondition: requires valid suit, faceValue and colour values
         * postcondition: the card is attached to a suit, face value and colour
         */
        public Card(Suit suit, FaceValue faceValue, Colour colour) {
            this.suit = suit;
            this.faceValue = faceValue;
            this.colour = colour;
        }


        /* Gets the face value of the card
         * precondition: true
         * postcondition: returns the face value of the card object
         */
        public FaceValue GetFaceValue() {
            return faceValue;
        }


        /* Gets the suit of the card
         * precondition: true
         * postcondition: returns the suit of the card object
         */
        public Suit GetSuit() {
            return suit;
        }


        /* Gets the colour of the card
         * precondition: true
         * postcondition: returns the colour of the card object
         */
        public Colour GetColour() {
            return colour;
        }


        /* Used to check if face value and suit is the same
         * precondition: card object for comparison
         * postcondition: returns true or false
         */
        public bool Equals(Card card) {
            return (this.suit == card.suit && this.faceValue == card.faceValue);
        }


        /* Used to check if face value or suit is the same
         * precondition: card object for comparison
         * postcondition: returns 0 if either is the same, otherwise returns -1
         */
        public int CompareTo(Card card) {
            if (this.faceValue == card.faceValue) {
                return 0;
            } else if (this.suit == card.suit) {
                return 0;
            } else {
                return -1;
            }
        }

    }
}

