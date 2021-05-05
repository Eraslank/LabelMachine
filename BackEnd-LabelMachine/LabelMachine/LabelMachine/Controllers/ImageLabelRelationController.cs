using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabelMachine.Models;
using LabelMachine.Models.Request;
using LabelMachine.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabelMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageLabelRelationController : Controller
    {
        private readonly ImageLabelRelationService _relationService;

        public ImageLabelRelationController(ImageLabelRelationService labelService)
        {
            _relationService = labelService;
            SingletonReferences.Instance.imageLabelRelationController = this;
        }
        [HttpGet]
        public ActionResult<List<ImageLabel>> Get() =>
            _relationService.Get();

        [HttpGet("{id:length(24)}", Name = "GetRelation")]
        public ActionResult<ImageLabel> Get(string id)
        {
            var rel = _relationService.Get(id);

            if (rel == null)
            {
                return NotFound();
            }

            return rel;
        }
        [NonAction]
        public IActionResult Create(CreateImageLabelRelationReq model)
        {
            foreach(var label in model.LabelIds)
            {
                _relationService.Create(new ImageLabel() { ImageId = model.ImageId, LabelId = label });
            }
            return Ok("Başarılı");
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var rel = _relationService.Get(id);

            if (rel == null)
            {
                return NotFound();
            }

            _relationService.Remove(rel.Id);

            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _relationService.RemoveAll();

            return Ok();
        }
    }
}
