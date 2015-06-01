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
     * Used to play the game Solitaire by
     * accessing objects from the SolitaireForm window
     * and the Solitaire_Game class
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public partial class SolitaireForm : Form {

        PictureBox[][] tables = new PictureBox[7][];
        PictureBox[][] pBoxRow = new PictureBox[7][];

        List<Card> flippedCards = new List<Card>();
        List<Card> storedCards = new List<Card>();

        List<TableLayoutPanel> tablePanels = new List<TableLayoutPanel>();


        public SolitaireForm() {
            InitializeComponent();
            AddTablesToList();
            Solitaire_Game.InitializeGame();
            drawPile.Image = Images.GetBackOfCardImage();
            SetupTables();
            DisplayCards();
        }


        //Add tables to the list
        private void AddTablesToList() {
            tablePanels.Add(tableau1);
            tablePanels.Add(tableau2);
            tablePanels.Add(tableau3);
            tablePanels.Add(tableau4);
            tablePanels.Add(tableau5);
            tablePanels.Add(tableau6);
            tablePanels.Add(tableau7);
        }


        //Display the cards at the start of the game
        private void DisplayCards() {
            foreach (TableLayoutPanel table in tablePanels) {
                for (int i = 0; i < tables[tablePanels.IndexOf(table)].Length; i++) {
                    table.Controls.Add(tables[tablePanels.IndexOf(table)][i]);
                }
            }
        }


        //Update suit piles
        private void updatePiles() {
            //update spade pile
            if (Solitaire_Game.GetSpadePile().GetCount() != 0) {
                spadeSuitPile.Image = Images.GetCardImage(Solitaire_Game.GetSpadePile().GetLastCardInPile());
            }

            //update diamond pile
            if (Solitaire_Game.GetDiamondPile().GetCount() != 0) {
                diamondSuitPile.Image = Images.GetCardImage(Solitaire_Game.GetDiamondPile().GetLastCardInPile());
            }

            //update club pile
            if (Solitaire_Game.GetClubPile().GetCount() != 0) {
                clubSuitPile.Image = Images.GetCardImage(Solitaire_Game.GetClubPile().GetLastCardInPile());
            }

            //update heart pile
            if (Solitaire_Game.GetHeartPile().GetCount() != 0) {
                heartSuitPile.Image = Images.GetCardImage(Solitaire_Game.GetHeartPile().GetLastCardInPile());
            }
        }


        //Add to the discard pile, create an event handler for the card and get the image
        private void drawPile_Click(object sender, EventArgs e) {
            Solitaire_Game.CheckDrawPile();
            Solitaire_Game.DrawCard();
            discardPile.Image = Images.GetCardImage(Solitaire_Game.GetDiscardPile().GetLastCardInPile());
            discardPile.Click += new EventHandler(PictureBox_Click);
            Card newCard = Solitaire_Game.GetDiscardPile().GetLastCardInPile();
            discardPile.Tag = newCard;
            flippedCards.Add(Solitaire_Game.GetDiscardPile().GetLastCardInPile());
        }
        
        
        //Set up tables
        private void SetupTables() {
            for (int i = 0; i < tables.Length; i++) {
                tables[i] = new PictureBox[15];
                for (int l = 0; l < tables[i].Length; l++) {
                    tables[i][l] = new PictureBox();
                }
            }
            for (int i = 0; i < pBoxRow.Length; i++) {
                pBoxRow[i] = new PictureBox[15];
            }

            //Set up the picture boxes and event handlers for each column
            for (int i = 0; i < Solitaire_Game.GetColumns().Length; i++) {
                Card cardValue;
                for (int k = 0; k < Solitaire_Game.GetColumns()[i].GetCount(); k++) {
                    pBoxRow[i][k] = new PictureBox();
                    pBoxRow[i][k].SizeMode = PictureBoxSizeMode.AutoSize;
                    pBoxRow[i][k].Dock = DockStyle.Fill;
                    cardValue = Solitaire_Game.GetColumns()[i].GetCard(k);
                    tables[i][k] = pBoxRow[i][k];
                    
                    //Make an event handler for the last card in the column
                    if (k == Solitaire_Game.GetColumns()[i].GetCount() - 1) {
                        tables[i][k].Click += new EventHandler(PictureBox_Click);
                        tables[i][k].Tag = Solitaire_Game.GetColumns()[i].GetCard(k);
                    }
                    //Get images for all the flipped cards
                    if (k == i || flippedCards.Contains(cardValue)) {
                        pBoxRow[i][k].Image = Images.GetCardImage(cardValue);
                        if (k == i) {
                            flippedCards.Add(cardValue);
                        }

                    } else {
                        pBoxRow[i][k].Image = Images.GetBackOfCardImage();
                    }
                }
            }
        }

        //Picutre box click event handler 
        /*** NOTE: THIS EVENT LOOPS FOR SOME REASON WHEN CLICKING THE DISCARD PILE *****************/
        /*** POSSIBLY DUE TO MULTIPLE EVENT HANDLERS CREATED FOR EACH CARD ON THE DISCARD PILE? ****/
        void PictureBox_Click(object sender, EventArgs e) {
            PictureBox clickedCard = (PictureBox)sender;
            Card whichCard = (Card)clickedCard.Tag;

            if (clickedCard.Image == Images.GetBackOfCardImage()) {
                ClearTables();
                flippedCards.Add(whichCard);
                UpdateImages();
            } else {
                Solitaire_Game.CheckCard(whichCard);
                CheckStartedPiles();
                //If the player has clicked 2 cards
                if (Solitaire_Game.GetCardClickedList().Count == 2) {
                    RemoveCard(Solitaire_Game.GetCardClickedList()[0]);
                    SearchCard(whichCard);
                    Solitaire_Game.GetCardClickedList().RemoveAt(1);
                    Solitaire_Game.GetCardClickedList().RemoveAt(0);
                    ClearTables();
                    UpdateImages();
                    Solitaire_Game.ChangeCardRemoved(false);
                }
                if (Solitaire_Game.GetCardRemoved() == true) {
                    MessageBox.Show("Not a valid move!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Solitaire_Game.ChangeCardRemoved(false);
                }
                if (Solitaire_Game.GetCardToTop() == true) {
                    ClearTables();
                    RemoveCard(whichCard);
                    UpdateImages();
                }     
            }
            if (Solitaire_Game.CheckWin()) {
                MessageBox.Show("YOU HAVE WON!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //Check if a pile has been started (create an event handler for it)
        public void CheckStartedPiles() {
            if (Solitaire_Game.GetClubPile().GetCount() != 0) {
                clubSuitPile.Click += new EventHandler(PictureBox_Click);
            }
            if (Solitaire_Game.GetDiamondPile().GetCount() != 0) {
                diamondSuitPile.Click += new EventHandler(PictureBox_Click);
            }
            if (Solitaire_Game.GetHeartPile().GetCount() != 0) {
                heartSuitPile.Click += new EventHandler(PictureBox_Click);
            }
            if (Solitaire_Game.GetSpadePile().GetCount() != 0) {
                spadeSuitPile.Click += new EventHandler(PictureBox_Click);
            }
        }


        //Clear all tables
        public void ClearTables() {
            tableau1.Controls.Clear();
            tableau2.Controls.Clear();
            tableau3.Controls.Clear();
            tableau4.Controls.Clear();
            tableau5.Controls.Clear();
            tableau6.Controls.Clear();
            tableau7.Controls.Clear();
        }


        //Update images
        public void UpdateImages() {
            updatePiles();
            SetupTables();
            DisplayCards();
        }


        //Search for the card and add another card to the same column 
        public void SearchCard(Card card) {
            for (int i = 0; i < Solitaire_Game.GetColumns().Length; i++) {
                if (Solitaire_Game.GetColumns()[i].ContainsCard(card)) {
                    Solitaire_Game.GetColumns()[i].AddCard(Solitaire_Game.GetCardClickedList()[0]);
                }
            }
        }


        //Search and remove the card from the columns or discard pile
        public void RemoveCard(Card card) {
            bool removedCard = false;
            if (Solitaire_Game.GetDiscardPile().GetCount() > 0) {
                if (Solitaire_Game.GetDiscardPile().GetLastCardInPile().GetFaceValue() == FaceValue.Ace || Solitaire_Game.GetCardClickedList()[0].Equals(Solitaire_Game.GetDiscardPile().GetLastCardInPile())) {
                    Solitaire_Game.GetDiscardPile().RemoveLastCard();
                    Card newCard = Solitaire_Game.GetDrawPile().DealOneCard();
                    Solitaire_Game.GetDiscardPile().AddCard(newCard);

                    discardPile.Image = Images.GetCardImage(newCard);
                    discardPile.Click += new EventHandler(PictureBox_Click);
                    discardPile.Tag = newCard;
                    removedCard = true;
                }
            }
            if (!removedCard) {
                for (int i = 0; i < Solitaire_Game.GetColumns().Length; i++) {
                    if (Solitaire_Game.GetColumns()[i].ContainsCard(card)) {
                        Solitaire_Game.GetColumns()[i].RemoveCard(card);
                    }
                }
            }
        }

    } //end class
}