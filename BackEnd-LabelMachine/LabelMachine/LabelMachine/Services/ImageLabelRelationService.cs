using LabelMachine.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Services
{
    public class ImageLabelRelationService : ControllerBase
    {
        private readonly IMongoCollection<ImageLabel> _imageLabelRelations;
        public ImageLabelRelationService(ILabelMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var dataBase = client.GetDatabase(settings.DatabaseName);
            _imageLabelRelations = dataBase.GetCollection<ImageLabel>(settings.ImageLabelRelationCollectionName);
        }
        public List<ImageLabel> Get() =>
            _imageLabelRelations.Find(rel => true).ToList();

        public ImageLabel Get(string id) =>
            _imageLabelRelations.Find(rel => rel.Id == id).FirstOrDefault();

        public bool Create(ImageLabel model)
        {
            try
            {
                _imageLabelRelations.InsertOne(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Update(string id, ImageLabel relIn) =>
            _imageLabelRelations.ReplaceOne(rel => rel.Id == id, relIn);
        public void Remove(string id) =>
            _imageLabelRelations.DeleteOne(rel => rel.Id == id);
        public void RemoveAll() =>
            _imageLabelRelations.DeleteMany(rel => true);
    }
}
