using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class StringValueAttribute : System.Attribute
    {

        private readonly string _value;
        private readonly Type _type;


        public StringValueAttribute(string value, Type type)
        {
            _value = value;
            _type = type;
        }

        public string Value
        {
            get { return _value; }
        }

        public Type Type
        {
            get { return _type; }
        }

    }
}
