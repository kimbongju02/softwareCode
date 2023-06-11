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
    public partial class CommentControl : UserControl
    {
        public CommentControl()
        {
            InitializeComponent();
        }
        List<FoodDB> list = new List<FoodDB>();
        FoodDB result = null;

        public void CommentRegister(String NickName, String Text, String FoodName)
        {
            list.Clear();
            list = FoodDB.getter(FoodName);//DB에서 가공식품이름 받아옴
            for (int i = 0; i < list.Count(); i++)
            {
                if ((list[i].Name)==FoodName)
                    result = list[i];
            }
            if (Text!="" || Text!=null)
            {
                FoodDB.AddComment(NickName, result.ReportNo, Text);

                
            }
            else
            {
                MessageBox.Show("입력을 해주세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CommentModify(String NickName, String Text, String FoodName)
        {
            list = FoodDB.getter(FoodName);//DB에서 가공식품이름 받아옴
            for (int i = 0; i < list.Count(); i++)
            {
                if ((list[i].Name)==FoodName)
                    result = list[i];
            }
            FoodDB.ModifyComment(NickName, result.ReportNo, Text);
        }
        public void CommentDelete(String NickName, String FoodName)
        {
            list = FoodDB.getter(FoodName);//DB에서 가공식품이름 받아옴
            for (int i = 0; i < list.Count(); i++)
            {
                if ((list[i].Name)==FoodName)
                    result = list[i];
            }
            FoodDB.DeleteComment(NickName,result.ReportNo, Text);
        }
        public FoodDB GetCommentInfo(String NickName, String FoodName)
        {
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
