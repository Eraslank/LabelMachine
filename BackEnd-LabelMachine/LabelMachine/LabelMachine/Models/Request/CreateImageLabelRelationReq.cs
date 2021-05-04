using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Models.Request
{
    public class CreateImageLabelRelationReq
    {
        public string ImageId { get; set; }
        public List<string> LabelIds{ get; set; }
    }
}
