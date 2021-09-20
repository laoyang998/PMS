using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Common.CustomAttributes
{
  [AttributeUsage(AttributeTargets.Class)]
  public  class PrimaryKeyAttribute:Attribute
    {
        public string Name { get; protected set; }
        public PrimaryKeyAttribute(string primaryKey)
        {
            this.Name = primaryKey;
        }
        public bool autoIncrement = false;
    }
}
