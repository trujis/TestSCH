using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;

namespace Users.Models
{
    public class RoleModel
    {

        private RolesEnum _id;
        public RolesEnum ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                Role = _id.GetDisplayAttributeFrom(typeof(RolesEnum));
            }
        }

        public string Role { get; private set; }


        public RoleModel()
        {
            
        }
    }
}
