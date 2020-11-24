using DropletCountWpfApp.Model;
using DropletCountWpfApp.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpfApp.DAL.Repository
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
            {  //rcgtemp make config
                 filePath = @"C:\DropletData\PlateDropletInfo.json"; // 96
               // filePath = @"C:\DropletData\PlateDropletInfo_48Wells.json"; // 48

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
