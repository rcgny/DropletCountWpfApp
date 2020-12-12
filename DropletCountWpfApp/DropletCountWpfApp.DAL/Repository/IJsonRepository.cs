using DropletCountWpf.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropletCountWpf.DAL.Repository
{
     public interface IJsonRepository
    {
        /// <summary>
        /// Returns the de-serialized JSON object 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>DropletBin</returns>
        DropletBin GetDropletBin(string filePath);
        
    }
}
