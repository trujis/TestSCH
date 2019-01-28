using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Models
{
    internal class Foo
    {

        public string UserName;
        public string Password;


        public override bool Equals(object obj)
        {
            return UserName == ((Foo)obj).UserName
                && Password == ((Foo)obj).Password;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
