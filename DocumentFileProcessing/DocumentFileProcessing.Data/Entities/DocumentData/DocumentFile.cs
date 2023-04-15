using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFileProcessing.Data.Entities.Identity;

namespace DocumentFileProcessing.Data.Entities.DocumentData
{
    public class DocumentFile : BaseEntity
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Int64 Length { get; set; }
    }
}
