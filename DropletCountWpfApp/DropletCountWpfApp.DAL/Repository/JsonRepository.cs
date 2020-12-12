using DropletCountWpf.Model;
using DropletCountWpf.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.DAL.Repository
{
    public class JsonRepository : IJsonRepository
    {     
        public JsonRepository( )
        {
           
        }
                
        public DropletBin GetDropletBin(string filePath)
        {
            DropletBin dropletBin = null;
            try
            {
                var rawJsonFile = File.ReadAllText(filePath);

                dropletBin = JsonConvert.DeserializeObject<DropletBin>(rawJsonFile);
            }
            catch (Exception ex)
            {
                //todo: Logging
            }
            return dropletBin;
        }       
    }
}
