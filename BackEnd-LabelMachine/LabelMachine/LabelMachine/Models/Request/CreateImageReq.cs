using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Models.Request
{
    public class CreateImageReq
    {
        public string FileName { get; set; }
        public string Base64Image { get; set; }
        public List<CreateLabelReq> Labels { get; set; }
    }
}
