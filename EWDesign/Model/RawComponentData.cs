using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Model
{
    public class RawComponentData
    {
        public string Type { get; set; }
        public Guid Id { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public List<RawComponentData> Children { get; set; }
    }
}
