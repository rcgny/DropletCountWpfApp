using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.Model
{
    [DataContract(Name = "Well")]
    public class Well : IComparable<Well>  // Adding this IF to enable sorting based on WellIndex, when Well is added to a List<Well>.
    {
        [DataMember]
        public string WellName { get; set; }

        [DataMember]
        public int WellIndex { get; set; }

        [DataMember]
        public int DropletCount { get; set; }

        public int CompareTo(Well other)
        {
            return this.WellIndex.CompareTo(other.WellIndex);
        }
    }
}
