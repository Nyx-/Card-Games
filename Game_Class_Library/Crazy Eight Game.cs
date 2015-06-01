using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Game_Class_Library;

namespace Game_Class_Library {

    /* 
     * Used to play the game Crazy Eights by
     * performing and executing general gameplay
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public static class Crazy_Eight_Game {

        private static int drawNumber = 8;

        private static CardPile draw;
        private static CardPile discard;

        private static Hand myHand;
        private static Hand compHand;

        private static bool alternateSuitEnabled = false;
        private static string alternateSuit;

        private static bool canPlay = false;
        private static bool cardPlaced = false;

        private static bool playPossible = false;
        private static bool alreadyDrawn = false;

        private static bool playerCantPlay = false;
        private static bool computerCantPlay = false;


        /***************** Accessors *****************/

        public static CardPile GetDrawPile() {
            return draw;
        }

        public static CardPile GetDiscardPile() {
            return discard;
        }

        public static Hand GetMyHand() {
            return myHand;
        }

        public static Hand GetCompHand() {
            return compHand;
        }

        public static string GetAlternateSuit() {
            return alternateSuit;
        }

        public static bool GetPlayerCantPlay() {
            return playerCantPlay;
        }

        public static bool GetCompCantPlay() {
            return computerCantPlay;
        }

        /*********************************************/


        //Initialize the game
        public static void InitializeGame() {
            draw = new CardPile(true);
            discard = new CardPile();
            draw.ShufflePile();
            myHand = new Hand(draw.DealCards(drawNumber));
            compHand = new Hand(draw.DealCards(drawNumber));
            myHand.SortHand();
            compHand.SortHand();
        }


        //Check if there are still any cards in the draw pile
        // if not, move the cards in the dicard pile, to the draw pile
        public static void CheckDrawPile() {
            if (draw.GetCount() == 0) {
                int numDiscard = discard.GetCount();
                for (int i = 0; i < numDiscard; i++) {
                    Card updatedCard = discard.DealOneCard();
                    draw.AddCard(updatedCard);
                }
                discard.AddCard(draw.DealOneCard());
            }
        }


        //Check each turn if either player has 0 cards in their hand
        // false = no winner        true = winner (stop the game)
        public static bool CheckWin() {
            if (myHand.GetCount() == 0) {
                return true;
            } else if (compHand.GetCount() == 0) {
                return true;
            } else {
                return false;
            }
        }


        //Check the card that the player has clicked
        public static string CheckPlayerCard(Card card) {
            //If an 8 hasn't been played
            if (!alternateSuitEnabled) {
                //If the first card which was flipped is an eight...
                if (discard.GetCount() == 1 && discard.GetLastCardInPile().GetFaceValue() == FaceValue.Eight) {
                    AddToDiscardPile(card, myHand);
                    if (card.GetFaceValue() == FaceValue.Eight) {
                        return "Eight";
                    } else {
                        return "OK";
                    }
                } else if (card.GetFaceValue() == FaceValue.Eight && alternateSuitEnabled == false) {
                    AddToDiscardPile(card, myHand);
                    alternateSuitEnabled = true;
                    return "Eight";
                //If the card clicked matches the card on the discard pile
                } else if (card.CompareTo(discard.GetLastCardInPile()) == 0 && alternateSuitEnabled == false) {
                    AddToDiscardPile(card, myHand);
                    return "OK";
                } else {
                    return "Not Playable";
                }
            //Else if an 8 has been played
            } else if (alternateSuitEnabled) {
                //If the card clicked matches the suit which the 8 was changed to
                if (card.GetSuit().ToString() == alternateSuit) {
                    AddToDiscardPile(card, myHand);
                    alternateSuitEnabled = false;
                    return "OK";
                } else {
                    return "Different Suit";
                }
            } else {
                return "Not Playable";
            }
        }


        //Check cards for the computer to play
        public static string CheckCompCard() {
            Card compSelected;
            canPlay = false;
            cardPlaced = false;
            computerCantPlay = false;

            for (int i = 0; i < compHand.GetCount(); i++) {
                compSelected = compHand.GetCard(i);
                //Checks if it can play a card of the same facevalue
                 if (compSelected.GetFaceValue() == discard.GetLastCardInPile().GetFaceValue() && cardPlaced == false && compSelected.GetFaceValue() != FaceValue.Eight && alternateSuitEnabled == false) {
                    AddToDiscardPile(compSelected, compHand);
                    canPlay = true;
                    cardPlaced = true;
                    return "OK";
                 //Checks if it can play a card of the same suit
                 } else if (compSelected.GetSuit() == discard.GetLastCardInPile().GetSuit() && cardPlaced == false && compSelected.GetFaceValue() != FaceValue.Eight && alternateSuitEnabled == false) {
                    AddToDiscardPile(compSelected, compHand);
                    canPlay = true;
                    cardPlaced = true;
                    return "OK";
                //If the player has played an eight, check if the computer player has a card of the same suit, or if it has an eight
                } else if (alternateSuitEnabled) {
                    if (compSelected.GetSuit().ToString() == alternateSuit || compSelected.GetFaceValue() == FaceValue.Eight) {
                        AddToDiscardPile(compSelected, compHand);
                        alternateSuitEnabled = false;
                        canPlay = true;
                        cardPlaced = true;
                        return "OK";
                    }
                //If it can't play anything else, play an eight
                } else if (canPlay == false) {
                    if (compSelected.GetFaceValue() == FaceValue.Eight) {
                        AddToDiscardPile(compSelected, compHand);
                        canPlay = true;
                        cardPlaced = true;
                        return "OK";
                    }     
                }
            }
            //If it doesn't have a playable card, draw one if permitted
            if (compHand.GetCount() < CardPile.NUM_CARDS_PER_SUIT) {
                DrawCard(compHand);
                return "Draw";
            } else {
                computerCantPlay = true;
                return "No Play";
            }
        }


        //Check if you can draw
        public static bool CheckDraw() {
            CheckDrawPile();           
            playPossible = false;
            alreadyDrawn = false;
            playerCantPlay = false;

            //If you have any playable cards in your hand
            for (int i = 0; i < myHand.GetCount(); i++) {
                Card playerSelected = myHand.GetCard(i);
                if (!alternateSuitEnabled) {
                    if (playerSelected.CompareTo(discard.GetLastCardInPile()) == 0 || playerSelected.GetFaceValue() == FaceValue.Eight) {
                        playPossible = true;
                        return true;
                    }
                } else if (alternateSuitEnabled) {
                    if (playerSelected.GetSuit().ToString() == alternateSuit) {
                        playPossible = true;
                        return true;
                    }
                }
            }
            //If you don't have any playable cards, draw a card if permitted
            if (myHand.GetCount() < CardPile.NUM_CARDS_PER_SUIT && alreadyDrawn == false && playPossible == false) {
                alreadyDrawn = true;
                DrawCard(myHand);
                return false;
            } else if (myHand.GetCount() == CardPile.NUM_CARDS_PER_SUIT) {
                playerCantPlay = true;
                return true;
            } else {
                return false;
            }
        }


        //Add a card to the discard pile and remove it from the hand it is from
        public static void AddToDiscardPile(Card card, Hand hand) {
            discard.AddCard(card);
            hand.RemoveCard(card);
        }

        //Mutator for alternatesuit
        public static void ChangeAlternateSuit(string suit) {
            alternateSuit = suit;
        }


        //Reset the game
        public static void ResetGame() {
            playerCantPlay = false;
            computerCantPlay = false;
            alternateSuitEnabled = false;
            alternateSuit = "";
            InitializeGame();
        }


        //Start the discard pile
        public static Card StartDiscardPile() {
            Card newCard = draw.GetLastCardInPile();
            discard.AddCard(newCard);
            draw.RemoveLastCard();
            return newCard;
        }


        //Draw a card
        public static void DrawCard(Hand hand) {
            Card newCard = draw.GetLastCardInPile();
            hand.AddCard(newCard);
            draw.RemoveLastCard();
        }

    }//end class
}
