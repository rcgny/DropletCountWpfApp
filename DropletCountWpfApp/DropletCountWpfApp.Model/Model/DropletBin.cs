using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.Model.Model
{
    [DataContract(Name = "DropletBin")]
    public class DropletBin
    {
        [DataMember]
        public PlateDropletInfo PlateDropletInfo { get; set; }
    }
}
