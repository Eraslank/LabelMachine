using LabelMachine.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Services
{
    public class LabelService: ControllerBase
    {
        private readonly IMongoCollection<Label> _labels;
        public LabelService(ILabelMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var dataBase = client.GetDatabase(settings.DatabaseName);
            _labels = dataBase.GetCollection<Label>(settings.LabelsCollectionName);
        }
        public List<Label> Get() =>
            _labels.Find(label => true).ToList();

        public Label Get(string id) =>
            _labels.Find<Label>(label => label.Id == id).FirstOrDefault();
        public Label GetByLabelName(string labelName) =>
            _labels.Find<Label>(label => label.LabelName == labelName).FirstOrDefault();
        public bool Create(Label model)
        {
            try
            {
                _labels.InsertOne(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Update(string id, Label labelIn) =>
            _labels.ReplaceOne(label => label.Id == id, labelIn);

        public void Remove(string id) =>
            _labels.DeleteOne(label => label.Id == id);
        public void RemoveAll() =>
            _labels.DeleteMany(image => true);
    }
}
