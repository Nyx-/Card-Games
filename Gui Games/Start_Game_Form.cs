using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gui_Games {
    public partial class Start_Game_Form : Form {
        public Start_Game_Form() {
            InitializeComponent();

            selectGameBox.Items.Add("Crazy Eights");
            selectGameBox.Items.Add("Solitaire");
        }

        private void selectGameBox_SelectedIndexChanged(object sender, EventArgs e) {
            startButton.Enabled = true;
        }

        private void startButton_Click(object sender, EventArgs e) {
            string gameSelect = selectGameBox.SelectedItem.ToString();
            if (gameSelect == "Crazy Eights") {
                //this.Hide();
                CrazyEightsForm crazyGame = new CrazyEightsForm();
                crazyGame.Show();
            } else {
                //this.Hide();
                SolitaireForm solitaireGame = new SolitaireForm();
                solitaireGame.Show();    
            }
        }

        private void exitButton_Click(object sender, EventArgs e) {
            Close();
        }

    }
}
