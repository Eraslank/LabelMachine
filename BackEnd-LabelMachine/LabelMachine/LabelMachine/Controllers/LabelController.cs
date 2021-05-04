using LabelMachine.Models;
using LabelMachine.Models.Request;
using LabelMachine.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly LabelService _labelService;

        public LabelController(LabelService labelService)
        {
            _labelService = labelService;

            SingletonReferences.Instance.labelController = this;
        }
        [HttpGet]
        public ActionResult<List<Label>> Get() =>
            _labelService.Get();

        [HttpGet("{id:length(24)}", Name = "GetLabel")]
        public ActionResult<Label> Get(string id)
        {
            var label = _labelService.Get(id);

            if (label == null)
            {
                return NotFound();
            }

            return label;
        }
        [NonAction]
        public Label GetByName(string labelName) =>
            _labelService.GetByLabelName(labelName);

        [NonAction]
        public bool CheckIfExist(string labelName)
        {
            var label = _labelService.GetByLabelName(labelName);
            return label != null;
        }
        [HttpPost]
        public IActionResult Create(CreateLabelReq model)
        {
            if (_labelService.Create(new Label(model)))
                return Ok("Başarılı");
            return BadRequest("Hata");
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, CreateLabelReq labelIn)
        {
            if (_labelService.Get(id) == null)
            {
                return NotFound();
            }

            _labelService.Update(id, new Label(labelIn));

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var label = _labelService.Get(id);

            if (label == null)
            {
                return NotFound();
            }

            _labelService.Remove(label.Id);

            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _labelService.RemoveAll();

            return Ok();
        }

        [NonAction]
        public void CreateLabelIfNotExist(string label)
        {
            if (!CheckIfExist(label))
                Create(new CreateLabelReq() { LabelName = label });
        }
        [NonAction]
        public void CreateLabelIfNotExist(List<CreateLabelReq> labels)
        {
            foreach (var label in labels)
            {
                if (!CheckIfExist(label.LabelName))
                    Create(label);
            }
        }
    }
}
