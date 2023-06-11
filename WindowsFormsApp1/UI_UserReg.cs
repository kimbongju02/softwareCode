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
    public partial class UI_UserReg : Menu
    {

        public UserReg userReg = new UserReg();
        public UI_UserReg()
        {
            InitializeComponent();
        }
        private void DoubleCheckButton(object sender, EventArgs e)
        {
            string nickName = textBox1.Text;
            bool re = userReg.DoubleCheck(nickName);
            if (!re)
            {
                MessageBox.Show("사용할 수 없는 닉네임입니다.", "Some title",MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text="";
            }
            else
            {
                MessageBox.Show("사용가능한 닉네임입니다.", "Some title",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisteButton(object sender, EventArgs e)//알레르기 비트값
        {
            int totalBit = 0b0000000000;
            int bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9;
            if (checkBox1.Checked==true)//난류
                bit1 = 0b0000000001;
            else
                bit1= 0b0000000000;
            if (checkBox2.Checked==true)//우유
                bit2= 0b0000000010;
            else
                bit2=0b0000000000;
            if (checkBox3.Checked==true)//곡류
                bit3=0b0000000100;
            else
                bit3=0b0000000000;
            if (checkBox4.Checked==true)//갑각류
                bit4=0b0000001000;
            else
                bit4=0b0000000000;
            if (checkBox5.Checked==true)//견과류
                bit5=0b0000010000;
            else
                bit5=0b0000000000;
            if (checkBox6.Checked==true)//생선류
                bit6=0b0000100000;
            else
                bit6=0b0000000000;
            if (checkBox8.Checked==true)//연체류
                bit7=0b0001000000;
            else
                bit7=0b0000000000;
            if (checkBox9.Checked==true)//육류
                bit8=0b0010000000;
            else
                bit8=0b0000000000;
            if (checkBox9.Checked==true)//대두류
                bit9=0b010000000;
            else
                bit9=0b0000000000;
            totalBit=bit1+bit2+bit3+bit4+bit5+bit6+bit7+bit8+bit9;

            userReg.SetUserInfo(NickName, textBox1.Text, totalBit);
            NickName=textBox1.Text;
        }

        new private void CancelButton(object sender, EventArgs e)
        {
            UserRegLoad(sender, e);
        }

        private void UserRegLoad(object sender, EventArgs e)
        {
            UserDB userDB = new UserDB();
            UserReg userReg = new UserReg();
            userDB = userReg.UserInfoLoad(NickName);
            textBox1.Text = userDB.nickName;
            if ((userDB.allergy&Allergic.Egg)!=0)
                checkBox1.Checked=true;
            if ((userDB.allergy&Allergic.Milk)!=0)
                checkBox2.Checked=true;
            if ((userDB.allergy&Allergic.Flour)!=0)
                checkBox3.Checked=true;
            if ((userDB.allergy&Allergic.Crab)!=0)
                checkBox4.Checked=true;
            if ((userDB.allergy&Allergic.Almond)!=0)
                checkBox5.Checked=true;
            if ((userDB.allergy&Allergic.Fish)!=0)
                checkBox6.Checked=true;
            if ((userDB.allergy&Allergic.Molluscs)!=0)
                checkBox7.Checked=true;
            if ((userDB.allergy&Allergic.Beef)!=0)
                checkBox8.Checked=true;
            if ((userDB.allergy&Allergic.Soybean)!=0)
                checkBox9.Checked=true;
        }
    }
}
