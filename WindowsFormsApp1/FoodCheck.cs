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
    public partial class FoodCheck : UserControl
    {
        public FoodCheck()
        {
            InitializeComponent();
        }
        public FoodDB LoadFoodInfo(String FoodName)
        {
            List<FoodDB> list = new List<FoodDB>();
            FoodDB result=null;
            list = FoodDB.getter(FoodName);//DB에서 가공식품이름 받아옴

            for (int i = 0; i < list.Count(); i++)
            {
                if ((list[i].Name)==FoodName)
                    result = list[i];
            }
            
            return result;
        }
    }
}
