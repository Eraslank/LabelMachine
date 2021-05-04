using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Models
{
   public class LabelMachineDatabaseSettings : ILabelMachineDatabaseSettings
    {
        public string ImagesCollectionName { get; set; }
        public string LabelsCollectionName { get; set; }
        public string ImageLabelRelationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILabelMachineDatabaseSettings
    {
        string ImagesCollectionName { get; set; }
        string LabelsCollectionName { get; set; }
        string ImageLabelRelationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
