using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp1
{
    public partial class UI_FoodCheck : Menu
    {
        public String SelectFoodName;
        public UI_FoodCheck(string selectFoodName)
        {
            InitializeComponent();
            SelectFoodName=selectFoodName;
        }

        private void FoodInfoLoad(object sender, EventArgs e)
        {
            FoodDB product = new FoodDB();
            FoodCheck foodCheck = new FoodCheck();
            product = foodCheck.LoadFoodInfo(SelectFoodName);

            textBox1.Text = product.Name;
            textBox3.Text = product.Manufacturer;
            foreach (var i in product.Comment)
                listBox2.Items.Add(i.Value);

            if ((product.Allergy&Allergic.Egg)!=0)
                listBox1.Items.Add("난류");
            if ((product.Allergy&Allergic.Milk)!=0)
                listBox1.Items.Add("우유");
            if ((product.Allergy&Allergic.Flour)!=0)
                listBox1.Items.Add("곡류");
            if ((product.Allergy&Allergic.Crab)!=0)
                listBox1.Items.Add("갑각류");
            if ((product.Allergy&Allergic.Almond)!=0)
                listBox1.Items.Add("견과류");
            if ((product.Allergy&Allergic.Fish)!=0)
                listBox1.Items.Add("생선류");
            if ((product.Allergy&Allergic.Molluscs)!=0)
                listBox1.Items.Add("연체류");
            if ((product.Allergy&Allergic.Beef)!=0)
                listBox1.Items.Add("육류");
            if ((product.Allergy&Allergic.Soybean)!=0)
                listBox1.Items.Add("대두");
            if (product.Allergy==Allergic.None)
                listBox1.Items.Add("없음");
        }
        private void CommentRegisterButtonClick(object sender, EventArgs e)
        {
            FoodDB product = new FoodDB();
            FoodCheck foodCheck = new FoodCheck();
            product = foodCheck.LoadFoodInfo(SelectFoodName);
            this.Visible=false;              // 추가

            UI_CommentReg moveCommentReg = new UI_CommentReg(textBox1.Text);

            moveCommentReg.ShowDialog();
        }
        private void CommentModifyButtonClick(object sender, EventArgs e)
        {
           
        }
        private void CommentDeleteButtonClick(object sender, EventArgs e)
        {
            
        }
    }
}
