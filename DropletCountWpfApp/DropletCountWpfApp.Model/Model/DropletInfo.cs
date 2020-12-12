using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.Model.Model
{
    [DataContract( Name = "DropletInfo")]
    public class DropletInfo
    {     

        [DataMember]
        public int  Version { get; set; }

        [DataMember]
        public List<Well> Wells { get; set; }
    }
}
