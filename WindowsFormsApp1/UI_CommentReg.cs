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
    public partial class UI_CommentReg : Menu
    {
        String selectFoodName;
        public UI_CommentReg( String selectFoodName)
        {
            InitializeComponent();
            this.selectFoodName=selectFoodName;
        }
        private void FoodCommentLoad(object sender, EventArgs e)
        {
            textBox1.Text="";
        }
        private void RegButtonClick(object sender, EventArgs e)
        {
            CommentControl commentControl=new CommentControl();
            commentControl.CommentRegister(NickName, textBox1.Text, selectFoodName);
            this.Visible=false;              // 추가

            UI_FoodSearch moveFoodSearch = new UI_FoodSearch();

            moveFoodSearch.ShowDialog();
        }
        private void CancelButton(object sender, EventArgs e)
        {
            this.Visible=false;              // 추가

            UI_FoodSearch moveFoodSearch = new UI_FoodSearch();

            moveFoodSearch.ShowDialog();
        }
    }
}
