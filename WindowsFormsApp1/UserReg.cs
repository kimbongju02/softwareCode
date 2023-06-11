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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class UserReg : UserControl
    {
        public static bool check = false;
        public UserReg()
        {
            InitializeComponent();
        }
        public bool DoubleCheck(String NickName)
        {
            bool re = UserDB.CheckNickName(NickName);
            if (NickName == null || NickName == "")
            {
                check = false;
                return false;
            }
            check = true;
            return re;
        }
        public void SetUserInfo(String NickName,String nickName, int Allergy)//알레르기 비트값
        {
            Allergic allergic;
            allergic=(Allergic)Allergy;
            if (check || NickName == nickName)
            {
                UserDB userDB = new UserDB();
                userDB.AddUserInfo(nickName, allergic);
                MessageBox.Show("등록되었습니다.", "Some title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("중복검사를 해주세요.", "Some title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            check=false;
        }

        public UserDB UserInfoLoad(String NickName)
        {
            check = false;
            return UserDB.getter(NickName);
        }
    }
}
