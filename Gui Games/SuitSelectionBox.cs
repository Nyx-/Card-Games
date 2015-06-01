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
    /* 
     * Used to select a suit value to
     * be used in the CrazyEightsForm
     * 
     * Author: Megan Hunter & Mitchell Atkinson
     * Date: May 2015
     * 
     */
    public partial class SuitSelectionBox : Form {
        string selectedValue;

        public SuitSelectionBox() {
            InitializeComponent();
        }

        private void rClub_CheckedChanged(object sender, EventArgs e) {
            selectedValue = rClub.Text;
        }

        private void rDiamonds_CheckedChanged(object sender, EventArgs e) {
            selectedValue = rDiamonds.Text;
        }

        private void rHearts_CheckedChanged(object sender, EventArgs e) {
            selectedValue = rHearts.Text;
        }

        private void rSpades_CheckedChanged(object sender, EventArgs e) {
            selectedValue = rSpades.Text;
        }

        public string returnSelection() {
            return selectedValue;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
