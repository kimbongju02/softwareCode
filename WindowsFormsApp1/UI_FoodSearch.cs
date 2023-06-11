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
    public partial class UI_FoodSearch : Menu
    {
        public UI_FoodSearch()
        {
            InitializeComponent();
        }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            FoodSearch foodSearch = new FoodSearch();
            List<FoodDB> foodName = new List<FoodDB>();
            //String FoodName = textBox1.Text;
            bool AllergyCheckBox = checkBox1.Checked;
            foodName = foodSearch.GetFoodName(textBox1.Text, AllergyCheckBox, NickName);
            foreach (FoodDB product in foodName)
            {
                listBox1.Items.Add(product.Name);
            }
        }
        private void FoodNameLoad(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            FoodSearch foodSearch = new FoodSearch();
            List<FoodDB> foodName = new List<FoodDB>();
            foodName = foodSearch.GetAllFoodName(NickName);
            foreach (FoodDB product in foodName)
            {
                listBox1.Items.Add(product.Name);
            }
        }
        private void FoodNameDoubleClick(object sender, EventArgs e)
        {
            string selectFoodName = listBox1.SelectedItem.ToString();

            this.Visible=false;

            UI_FoodCheck uI_FoodCheck = new UI_FoodCheck(selectFoodName);
            uI_FoodCheck.ShowDialog();
        }
    }
}
