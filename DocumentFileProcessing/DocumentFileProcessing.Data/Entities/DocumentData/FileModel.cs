using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFileProcessing.Data.Entities.Identity;

namespace DocumentFileProcessing.Data.Entities.DocumentData
{
    public abstract  class FileModel : BaseEntity
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
    }
}
