using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS
{
    public static class FormUtility
    {
        public static void TryCatch(this Action act, string message)
        {
            try
            {
                act.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, message, MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //关闭显示在选项卡中的窗体
        public static void CloseForm(this Form form)
        {
            TabPage tp = form.Parent as TabPage;
            TabControl tc = tp.Parent as TabControl;
            tc.TabPages.Remove(tp);
            form.Close();
        }

        //检查是否有重复窗体
        public static bool CheckOpenForm(string formname)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == formname)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }

        //关闭指定的窗体
        public static void CloseOpenForm(string FormName)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == FormName)
                {
                    frm.Close();
                    break;
                }
            }
        }

        //添加窗体页面到选项卡中
        public static void AddTabFormPage(this TabControl tab, Form form)
        {
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            TabPage page = new TabPage();
            page.Controls.Add(form);
            page.Name = form.Name;
            page.Text = form.Text;
            page.Width = form.Width;
            page.Height = form.Height;
            tab.TabPages.Add(page);
            form.Show();
        }
    }
}
