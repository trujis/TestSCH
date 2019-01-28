using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Enums
{
    public enum RolesEnum : int
    {

        [StringValue("ADMIN", typeof(RolesEnum))]
        ADMIN,

        [StringValue("PAGE_1", typeof(RolesEnum))]
        PAGE_1,

        [StringValue("PAGE_2", typeof(RolesEnum))]
        PAGE_2,

        [StringValue("PAGE_3", typeof(RolesEnum))]
        PAGE_3

    }
}
