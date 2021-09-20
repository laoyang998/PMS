using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMS.Common;
using DBUnity;
using System.Data.SqlClient;
using System.Collections;
using PMS.BLL;
using PMS.Models.VModels;

namespace PMS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Action act = () =>
            {
                string uname = txtUserName.Text;
                string upwd = Encryption.GetMD5Str64(txtPassword.Text);
                //登录检查 
                UserBLL userBLL = new UserBLL();
                List<ViewUserRoleModel> urList = userBLL.Login(uname, upwd);
                if (urList == null || urList.Count == 0)
                {
                    MessageBox.Show(this, "帐号或密码输入有误，请检查", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!FormUtility.CheckOpenForm("frmMain"))
                    {
                        frmMain fMain = new frmMain();
                        fMain.Tag = new FModels.LoginModel()
                        {
                            URList = urList,
                            loginForm = this
                        };

                        fMain.Show();
                    }
                    else
                    {
                        //更换用户（待补充）
                    }

                    this.Hide();
                }
            };

            act.TryCatch("登录出现异常");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
