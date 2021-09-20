using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMS.FModels;
using PMS.Models.VModels;
namespace PMS
{

    public partial class frmMain : Form
    {
        List<ViewUserRoleModel> urList = null;
        string uName;
        bool isAdmin = false;

        public frmMain()
        {
            InitializeComponent();

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormUtility.CloseOpenForm("frmLogin");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Action act = () =>
            {
                if (this.Tag != null)
                {
                    InitMainInfo();
                }
            };

            act.TryCatch("主页面初始化发生异常");
        }


        //初始化
        private void InitMainInfo()
        {
            LoginModel loginModel = this.Tag as LoginModel;
            if (loginModel != null)
            {
                urList = loginModel.URList;
                uName = urList[0].UserName;
                CheckIsAdmin();
                if (isAdmin) //超级管理员
                {
                    //加载所有菜单和工具栏
                }
                else
                {
                    //加载登录拥有的菜单和工具栏
                    string roleIds = string.Join(",", urList.Select(ur => ur.RoleId));
                }
            }
        }

        //检查是否为管理员
        private void CheckIsAdmin()
        {
            foreach (var ur in urList)
            {
                if (ur.UserName == "admin")
                {
                    isAdmin = true;
                    break;
                }
            }
        }
    }
}
