using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.Model
{
    public class Tag
    {
        public string Name { get; set; }
        public long DbId { get; set; }
        public Tag(long dbId,string name)
        {
            DbId = dbId;
            Name = name;
        }
    }
}
