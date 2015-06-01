using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Game_Class_Library;

namespace Game_Class_Library {

    /* 
     * Used to play the game Solitaire by
     * performing and executing general gameplay
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public static class Solitaire_Game {

        private static CardPile draw;
        private static CardPile discard;
        private static CardPile clubPile;
        private static CardPile spadePile;
        private static CardPile heartPile;
        private static CardPile diamondPile;

        private static bool cardRemoved = false;
        private static bool valid = false;

        private static Hand[] columns = new Hand[7];

        private static bool cardToTop = false;

        private static List<Card> cardClicked = new List<Card>();

        private static string[] faceValues = FaceValue.GetNames(typeof(FaceValue));

        
        /***************** Accessors *****************/

        public static CardPile GetDrawPile() {
            return draw;
        }

        public static CardPile GetDiscardPile() {
            return discard;
        }

        public static CardPile GetClubPile() {
            return clubPile;
        }

        public static CardPile GetSpadePile() {
            return spadePile;
        }

        public static CardPile GetHeartPile() {
            return heartPile;
        }

        public static CardPile GetDiamondPile() {
            return diamondPile;
        }

        public static bool GetCardRemoved() {
            return cardRemoved;
        }

        public static Hand[] GetColumns() {
            return columns;
        }

        public static bool GetCardToTop() {
            return cardToTop;
        }

        public static List<Card> GetCardClickedList() {
            return cardClicked;
        }


        /*********************************************/

        //Mutator for cardremoved
        public static void ChangeCardRemoved(bool value) {
            cardRemoved = value;
        }


        //Startup game
        public static void InitializeGame() {
            draw = new CardPile(true);
            discard = new CardPile();
            draw.ShufflePile();
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new Hand(draw.DealCards(i+1));
            }            

            clubPile = new CardPile();
            spadePile = new CardPile();
            heartPile = new CardPile();
            diamondPile = new CardPile();
        }


        //Check if the draw pile is empty
        public static void CheckDrawPile() {
            if (draw.GetCount() == 0) {
                int numDiscard = discard.GetCount();
                for (int i = 0; i < numDiscard; i++) {
                    Card updatedCard = discard.DealOneCard();
                    draw.AddCard(updatedCard);
                }
            }
        }


        //Draw a card
        public static void DrawCard() {
            Card newCard = draw.GetLastCardInPile();
            discard.AddCard(newCard);
            draw.RemoveLastCard();
        }


        //Check if all the suit piles have been filled - i.e. winning the game
        public static bool CheckWin() {
            if (heartPile.GetCount() == CardPile.NUM_CARDS_PER_SUIT &&
                clubPile.GetCount() == CardPile.NUM_CARDS_PER_SUIT &&
                diamondPile.GetCount() == CardPile.NUM_CARDS_PER_SUIT &&
                spadePile.GetCount() == CardPile.NUM_CARDS_PER_SUIT) {
                return true;
            } else {
                return false;
            }
        }


        //Check what card was clicked
        public static void CheckCard(Card card) {
            cardToTop = false;

            if (card != null) {
                if (card.GetFaceValue() == FaceValue.Ace) {
                    AceMethod(card);
                } else {
                    AddToArray(card);
                }
            } else {
                cardClicked.RemoveAt(0);
            }
        }


        //Add card to the clicked card array, or add to suit piles
        public static void AddToArray(Card card) {
            //If there are no cards in the list
            if (cardClicked.Count == 0) {
                cardClicked.Add(card);
            } else if (cardClicked.Count > 0) {
                //If the card in the list is of a different colour to the card clicked
                if (cardClicked[0].GetColour() != card.GetColour()) {
                    for (int i = 0; i < faceValues.Length - 1; i++) {
                        //If the facevalue of the card clicked is 1 above the card in the list...
                        if (faceValues[i] == cardClicked[0].GetFaceValue().ToString() && card.GetFaceValue().ToString() == faceValues[i + 1]) {
                            
                            // Check if the suit piles have been started
                            if (clubPile.GetCount() > 0) {
                                if (CheckPile(card, clubPile)) {
                                    break;
                                }
                            } else if (diamondPile.GetCount() > 0) {
                                if (CheckPile(card, diamondPile)) {
                                    break;
                                }
                            } else if (heartPile.GetCount() > 0) {
                                if (CheckPile(card, heartPile)) {
                                    break;
                                }
                            } else if (spadePile.GetCount() > 0) {
                                if (CheckPile(card, spadePile)) {
                                    break;
                                }
                            } else {
                                cardClicked.Add(card);
                                valid = true;
                                break;
                            }
                        }
                    }
                    //If the card isn't valid, clear the list
                    if (valid == false) {
                        cardClicked.RemoveAt(0);
                        cardRemoved = true;
                    }
                //If the card is the same colour, clear the list
                } else {
                    cardClicked.RemoveAt(0);
                    cardRemoved = true;
                }
            }
        }
        

        //Check if a pile has been started
        public static bool CheckPile(Card card, CardPile pile) {
            if (card == pile.GetLastCardInPile()) {
                CheckFaces(card);
                cardClicked.RemoveAt(0);
                valid = true;
                return true;
            } else {
                cardClicked.Add(card);
                valid = true;
                return true;
            }
        }


        //check the face values of the card clicked
        public static void CheckFaces(Card card) {
            switch (card.GetFaceValue()) {
                case FaceValue.Two :
                    CardMethod(card, "Ace");
                    break;
                case FaceValue.Three :
                    CardMethod(card, "Two");
                    break;
                case FaceValue.Four :
                    CardMethod(card, "Three");
                    break;
                case FaceValue.Five :
                    CardMethod(card, "Four");
                    break;
                case FaceValue.Six :
                    CardMethod(card, "Five");
                    break;
                case FaceValue.Seven :
                    CardMethod(card, "Six");
                    break;
                case FaceValue.Eight :
                    CardMethod(card, "Seven");
                    break;
                case FaceValue.Nine :
                    CardMethod(card, "Eight");
                    break;
                case FaceValue.Ten :
                    CardMethod(card, "Nine");
                    break;
                case FaceValue.Jack :
                    CardMethod(card, "Ten");
                    break;
                case FaceValue.Queen :
                    CardMethod(card, "Jack");
                    break;
                case FaceValue.King :
                    CardMethod(card, "Queen");
                    break;
            }
        }


        // If the card clicked was an Ace
        private static void AceMethod(Card card) {
            if (card.GetSuit() == Suit.Clubs) {
                if (clubPile.GetCount() == 0) {
                    clubPile.AddCard(card);
                }  
            }
            if (card.GetSuit() == Suit.Diamonds) {
                if (diamondPile.GetCount() == 0) {
                    diamondPile.AddCard(card);
                }  
            }
            if (card.GetSuit() == Suit.Hearts) {
                if (heartPile.GetCount() == 0) {
                    heartPile.AddCard(card);
                }      
            }
            if (card.GetSuit() == Suit.Spades) {
                if (spadePile.GetCount() == 0) {
                    spadePile.AddCard(card);
                }  
            }
            cardToTop = true;
        }


        //If the card clicked is a three
        private static void CardMethod(Card card, string face) {
            if (card.GetSuit() == Suit.Clubs) {
                if (clubPile.GetLastCardInPile().GetFaceValue().ToString() == face) {
                    clubPile.AddCard(card);
                    cardToTop = true;
                }
            }
            if (card.GetSuit() == Suit.Diamonds) {
                if (diamondPile.GetLastCardInPile().GetFaceValue().ToString() == face) {
                    diamondPile.AddCard(card);
                    cardToTop = true;
                }
            }
            if (card.GetSuit() == Suit.Hearts) {
                if (heartPile.GetLastCardInPile().GetFaceValue().ToString() == face) {
                    heartPile.AddCard(card);
                    cardToTop = true;
                }
            }
            if (card.GetSuit() == Suit.Spades) {
                if (spadePile.GetLastCardInPile().GetFaceValue().ToString() == face) {
                    spadePile.AddCard(card);
                    cardToTop = true;
                }
            }
        }
    }
}
