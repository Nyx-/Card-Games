using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GuiGames;
using Shared_Game_Class_Library;
using Game_Class_Library;

namespace Gui_Games {

    /* 
     * Used to play the game Crazy Eights by
     * accessing objects from the CrazyEightForm window
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public partial class CrazyEightsForm : Form {

        PictureBox[] computerHand;
        PictureBox[] myHand;

        bool hasDealt = false;
        bool gameEnded = false;

        bool winner = false;

        public CrazyEightsForm() {
            InitializeComponent();
            drawPile.Image = Images.GetBackOfCardImage();
            computerHand = new PictureBox[CardPile.NUM_CARDS_PER_SUIT];
            myHand = new PictureBox[CardPile.NUM_CARDS_PER_SUIT];
        }


        //Sets up all the images on the page
        //For both computer and player hands
        private void SetUpCardImages() {

            UpdateComputerImages();
            UpdatePlayerImages();

            DisplayComputerCards();
            DisplayPlayerCards();

        }

        //Sets up the picture boxes for the computer's hand
        private void UpdateComputerImages() {
            PictureBox pBoxComp;
            Card cardValue;

            for (int column = 0; column < Crazy_Eight_Game.GetCompHand().GetCount(); column++) {
                pBoxComp = new PictureBox();
                pBoxComp.SizeMode = PictureBoxSizeMode.AutoSize;
                pBoxComp.Dock = DockStyle.Fill;
                cardValue = Crazy_Eight_Game.GetCompHand().GetCard(column);
                pBoxComp.Image = Images.GetCardImage(cardValue);
                computerHand[column] = pBoxComp;
            }
        }

        //Sets up the picture boxes for the player's hand
        //Creates events and tags for each of the picture boxes
        //  to make them clickable
        private void UpdatePlayerImages() {
            PictureBox pBoxPlayer;
            Card cardValue;

            for (int column = 0; column < Crazy_Eight_Game.GetMyHand().GetCount(); column++) {
                pBoxPlayer = new PictureBox();
                pBoxPlayer.SizeMode = PictureBoxSizeMode.AutoSize;
                pBoxPlayer.Dock = DockStyle.Fill;
                cardValue = Crazy_Eight_Game.GetMyHand().GetCard(column);
                pBoxPlayer.Image = Images.GetCardImage(cardValue);
                myHand[column] = pBoxPlayer;

                myHand[column].Click += new EventHandler(PictureBox_Click);
                myHand[column].Tag = Crazy_Eight_Game.GetMyHand().GetCard(column);
            }
        }


        //Picturebox event handers when clicked
        //checks if the clicked card matches the card on the pile by both suit and facevalue
        //updates the discard pile, player's hand and images
        void PictureBox_Click(object sender, EventArgs e) {

            PictureBox clickedCard = (PictureBox)sender;
            Card whichCard = (Card)clickedCard.Tag;
            if (!winner) {
                switch (Crazy_Eight_Game.CheckPlayerCard(whichCard)) {
                    case "Eight":
                        SuitSelectionBox form = new SuitSelectionBox();
                        form.ShowDialog();
                        Crazy_Eight_Game.ChangeAlternateSuit(form.returnSelection());
                        discardPile.Image = Images.GetCardImage(Crazy_Eight_Game.GetDiscardPile().GetLastCardInPile());
                        FinishTurn(playerHand, "Player");
                        if (!winner) {
                            ComputerPlay();
                            break;
                        } else {
                            instructionsText.Text = " Click deal to play again.";
                            break;
                        }
                        

                    case "OK":
                        FinishTurn(playerHand, "Player");
                        discardPile.Image = Images.GetCardImage(Crazy_Eight_Game.GetDiscardPile().GetLastCardInPile());
                        if (!winner) {
                            ComputerPlay();
                            break;
                        } else {
                            instructionsText.Text = " Click deal to play again.";
                            break;
                        }

                    case "Different Suit":
                        instructionsText.Text = "You must place a " + Crazy_Eight_Game.GetAlternateSuit();
                        break;

                    case "Not Playable":
                        instructionsText.Text = " Can't play that card now.";
                        break;
                }
            }
        }


        //The computer has it's turn
        private void ComputerPlay() {
            Crazy_Eight_Game.CheckDrawPile();
            switch (Crazy_Eight_Game.CheckCompCard()) {
                case "OK":
                    discardPile.Image = Images.GetCardImage(Crazy_Eight_Game.GetDiscardPile().GetLastCardInPile());
                    FinishTurn(compHand, "Computer");
                    if (!winner) {
                        instructionsText.Text = " Your turn. Click on one of your cards to play.";
                        break;
                    } else {
                        instructionsText.Text = " Click deal to play again.";
                        break;
                    }
                case "Draw":
                    FinishTurn(compHand, "Computer");
                    ComputerPlay();
                    break;
                case "No Play":
                    if (Crazy_Eight_Game.GetPlayerCantPlay() && Crazy_Eight_Game.GetCompCantPlay()) {
                        MessageBox.Show("No More Moves Available!", "Draw", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else {
                        instructionsText.Text = " Your turn. Click on one of your cards to play.";
                    }
                    break;
            }
        }

        //Displays the images of the computer's hand of cards
        private void DisplayComputerCards() {
            compHand.Controls.Clear();

            for (int column = 0; column < Crazy_Eight_Game.GetCompHand().GetCount(); column++) {
                compHand.Controls.Add(computerHand[column]);
            }
        }

        //Displays the images of the players's hand of cards
        private void DisplayPlayerCards() {
            playerHand.Controls.Clear();

            for (int column = 0; column < Crazy_Eight_Game.GetMyHand().GetCount(); column++) {
                playerHand.Controls.Add(myHand[column]);
            }
        }

        //Sets up the discard pile by drawing and displaying the next card from the draw pile
        private void InitializeDiscardPile() {
            discardPile.Image = Images.GetCardImage(Crazy_Eight_Game.StartDiscardPile());
        }


        //Adds text to the textbox when the window opens
        private void CrazyEightsForm_Load(object sender, EventArgs e) {
            instructionsText.Text = "Click Deal to start the game.";
        }


        //Alters text in text box and enables the sort button
        //Initializes the Game (cards, hands, etc.)
        //Initializes the images
        private void dealButton_Click(object sender, EventArgs e) {
            deal();
        }

        //Closes the window
        private void cancelButton_Click(object sender, EventArgs e) {
            sortButton.Enabled = false;
            Close();
        }

        //Sorts the players hand only 
        // (something weird happens when you click it again and
        //      sometimes it doesn't always sort first go?)
        private void sortButton_Click(object sender, EventArgs e) {
            Crazy_Eight_Game.GetMyHand().SortHand();
            UpdatePlayerImages();
            DisplayPlayerCards();
        }


        //Only if you have nothing else to play can you take a card
        // from the draw pile
        private void drawPile_Click(object sender, EventArgs e) {
            if (hasDealt && !gameEnded) {
                Crazy_Eight_Game.CheckDrawPile();
                discardPile.Image = Images.GetCardImage(Crazy_Eight_Game.GetDiscardPile().GetLastCardInPile());
                if (Crazy_Eight_Game.GetDrawPile().GetCount() != 0) {
                    bool Draw = Crazy_Eight_Game.CheckDraw();

                    if (Draw == true) {
                        instructionsText.Text = " Cannot Draw now. You still have at least one card you can play.";
                    } else if (Draw == false) {
                        UpdatePlayerImages();
                        DisplayPlayerCards();
                    }
                    if (Crazy_Eight_Game.GetPlayerCantPlay()) {
                        if (Crazy_Eight_Game.GetCompCantPlay()) {
                            MessageBox.Show("No More Moves Available!", "Draw", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gameEnded = true;
                        } else {
                            ComputerPlay();
                        }
                    }
                }
            }
        }


        //Finish up the turn by refreshing all the images
        private void FinishTurn(TableLayoutPanel hand, string user) {
            hand.Controls.Clear();
            if (user == "Player") {
                UpdatePlayerImages();
                DisplayPlayerCards();
            } else if (user == "Computer") {
                UpdateComputerImages();
                DisplayComputerCards();
            }
            CheckScore();
        }


        //Check to see if there is a winner or not
        public void CheckScore() {
            if (Crazy_Eight_Game.CheckWin()) {
                if (Crazy_Eight_Game.GetMyHand().GetCount() == 0) {
                    MessageBox.Show("You win!", "Winner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (Crazy_Eight_Game.GetCompHand().GetCount() == 0) {
                    MessageBox.Show("The computer won!", "Winner", MessageBoxButtons.OK, MessageBoxIcon.Stop);   
                }
                gameEnded = true;
                winner = true;
            }   
        }


        //Deal the cards to start a new game
        public void deal() {
            if (winner) {
                instructionsText.Text = "New game started. Your turn...";
            } else {
                instructionsText.Text = " Your turn. Click on one of your cards to play.";
            }
            gameEnded = false;
            sortButton.Enabled = true;
            hasDealt = true;
            winner = false;
            Crazy_Eight_Game.ResetGame();
            SetUpCardImages();
            InitializeDiscardPile();
        }
    }
}
