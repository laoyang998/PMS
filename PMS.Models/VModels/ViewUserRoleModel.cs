﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Common.CustomAttributes;

namespace PMS.Models.VModels
{
    [Table("ViewUserRoleInfos")]
    public class ViewUserRoleModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
