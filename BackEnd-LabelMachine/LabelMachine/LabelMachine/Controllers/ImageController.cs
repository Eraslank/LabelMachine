using LabelMachine.Models;
using LabelMachine.Models.Request;
using LabelMachine.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabelMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;

        public ImageController(ImageService labelService)
        {
            _imageService = labelService;
            SingletonReferences.Instance.imageController = this;
        }
        [HttpGet]
        public ActionResult<List<Image>> Get() =>
            _imageService.Get();

        [HttpGet("{id:length(24)}", Name = "GetImage")]
        public ActionResult<Image> Get(string id)
        {
            var image = _imageService.Get(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        [HttpPost]
        public IActionResult Create(CreateImageReq model)
        {
            var image = new Image(SaveBase64AsImage(model.FileName, model.Base64Image));
            _imageService.Create(image);
            List<string> labelIds = new List<string>();
            foreach(var label in SingletonReferences.Instance.labelController.CreateLabelIfNotExist(model.Labels))
            {
                labelIds.Add(label);
            }
            SingletonReferences.Instance.imageLabelRelationController.Create(new CreateImageLabelRelationReq() { ImageId = image.Id, LabelIds = labelIds });
            return Ok("Başarılı");
            //Create Relationship
            //SingletonReferences.Instance.labelController.GetByName()
            /*
                return Ok("Başarılı");
            return BadRequest("Hata");
            */
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, CreateImageReq imageIn)
        {
            if (_imageService.Get(id) == null)
            {
                return NotFound();
            }

            var image = new Image(SaveBase64AsImage(imageIn.FileName, imageIn.Base64Image));
            _imageService.Update(id, image);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var image = _imageService.Get(id);

            if (image == null)
            {
                return NotFound();
            }

            _imageService.Remove(image.Id);

            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _imageService.RemoveAll();

            return Ok();
        }
        private string SaveBase64AsImage(string fileName, string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }
            fileName += ".png";
            var path = Path.Combine(Directory.GetCurrentDirectory(),"Images");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, fileName);
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            return path;
        }

    }
}
