using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentFileProcessing.Data.Entities.DocumentData
{
    public class FileOnDataBaseModel : FileModel
    {
        public byte[] Data { get; set; }
    }
}
