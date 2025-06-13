using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditablePropertyAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public EditablePropertyAttribute(string displayName = "")
        {
            DisplayName = displayName;
        }
    }
}
