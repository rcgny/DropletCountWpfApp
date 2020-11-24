using DropletCountWpfApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpfApp.DAL.Repository
{
     public interface IJsonRepository
    {
        /// <summary>
        /// Returns the de-serialized JSON object 
        /// </summary>
        /// <returns></returns>
        DropletBin GetDropletBin(string filePath);
    }
}
