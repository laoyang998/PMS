using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models.VModels;

namespace PMS.FModels
{
  public  class LoginModel
    {
        public List<ViewUserRoleModel> URList { get; set; }
        public  frmLogin loginForm{ get; set; }
    }
}
