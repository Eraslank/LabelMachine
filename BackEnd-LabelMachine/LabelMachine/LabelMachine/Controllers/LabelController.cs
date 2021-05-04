using LabelMachine.Models;
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
    public class LabelController: ControllerBase
    {
        private readonly LabelService _labelService;

        public LabelController(LabelService labelService)
        {
            _labelService = labelService;
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

        [HttpPost]
        public IActionResult Create(Label model)
        {
            if (_labelService.Create(model))
                return Ok("Başarılı");
            return BadRequest("Hata");
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Label labelIn)
        {
            var label = _labelService.Get(id);

            if (label == null)
            {
                return NotFound();
            }

            _labelService.Update(id, labelIn);

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

    }
}
