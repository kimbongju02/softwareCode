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
    public partial class FoodSearch : UserControl
    {
        public FoodSearch()
        {
            InitializeComponent();
        }
        public List<FoodDB> GetFoodName(String FoodName, bool AllergyCheckBox, String NickName)
        {
            List<FoodDB> list = new List<FoodDB>();
            list = FoodDB.getter(FoodName);//DB에서 가공식품이름 받아옴

            UserDB userDB = new UserDB();
            userDB=UserDB.getter(NickName);//DB에서 사용자 정보 받아옴

            int userAllergy = (int)userDB.allergy;//사용자의 알레르기 정보
            if (AllergyCheckBox)
            {
                for (int i = 0; i < list.Count(); i++)
                {//가져온 리스트의 알레르기 정보(A)와 사용자의 알레르기 정보(B) 비교
                    if (((int)list[i].Allergy&userAllergy)!=0)//A==B
                        list.RemoveAt(i);
                }
            }
            return list;
        }

        public List<FoodDB> GetAllFoodName(String NickName)
        {
            List<FoodDB> list = new List<FoodDB>();
            list = FoodDB.getter();//DB에서 가공식품이름 받아옴

            UserDB userDB = new UserDB();
            userDB=UserDB.getter(NickName);//DB에서 사용자 정보 받아옴

            int userAllergy = (int)userDB.allergy;//사용자의 알레르기 정보
            for (int i = 0; i < list.Count(); i++)
            {//가져온 리스트의 알레르기 정보(A)와 사용자의 알레르기 정보(B) 비교
                if (((int)list[i].Allergy&userAllergy)!=0)//A==B
                    list.RemoveAt(i);
            }
            return list;
        }
    }
}
