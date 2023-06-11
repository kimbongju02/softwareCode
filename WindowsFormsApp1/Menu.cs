using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Menu : Form
    {
        public static String NickName = "김봉주";
        public Menu()
        {
            InitializeComponent();
            //NickName = "김봉주";
        }
        private void MoveFoodSearch(object sender, EventArgs e)
        {
            this.Visible=false;              // 추가

            UI_FoodSearch moveFoodSearch = new UI_FoodSearch();

            moveFoodSearch.ShowDialog();
        }

        private void MoveRecipeSearch(object sender, EventArgs e)
        {
        }

        private void MoveUserInfo(object sender, EventArgs e)
        {
            this.Visible=false;           // 추가

            UI_UserReg moveUserInfo = new UI_UserReg();

            moveUserInfo.ShowDialog();
        }
        private void MoveHopitalSearch()
        {

        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void 병원검색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
