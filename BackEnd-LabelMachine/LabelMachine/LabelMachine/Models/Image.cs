using LabelMachine.Models.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId)]
        public string Id { get; set; }
        public string ImageUrl { get; set; }

        public Image(string filePath)
        {
            this.ImageUrl = filePath;
        }
    }
}
