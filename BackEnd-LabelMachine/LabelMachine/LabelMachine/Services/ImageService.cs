using LabelMachine.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Services
{
    public class ImageService : ControllerBase
    {
        private readonly IMongoCollection<Image> _images;
        public ImageService(ILabelMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var dataBase = client.GetDatabase(settings.DatabaseName);
            _images = dataBase.GetCollection<Image>(settings.ImagesCollectionName);
        }
        public List<Image> Get() =>
            _images.Find(image => true).ToList();

        public Image Get(string id) =>
            _images.Find<Image>(image => image.Id == id).FirstOrDefault();
        public bool Create(Image model)
        {
            try
            {
                _images.InsertOne(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Update(string id, Image imageIn) =>
            _images.ReplaceOne(image => image.Id == id, imageIn);

        public void Remove(string id) =>
            _images.DeleteOne(image => image.Id == id);
        public void RemoveAll() =>
            _images.DeleteMany(image => true);
    }
}
