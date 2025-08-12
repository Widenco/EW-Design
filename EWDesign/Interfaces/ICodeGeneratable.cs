using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Interfaces
{
    public interface ICodeGeneratable
    {
        string HTMLContent(string className);
        string CSSContent(string className);
    }

}
