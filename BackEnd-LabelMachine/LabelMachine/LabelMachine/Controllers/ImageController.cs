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
            var image = new Image(SaveByteArrayAsImage(model.FileName, model.Base64Image));
            if (_imageService.Create(image))
                return Ok("Başarılı");
            return BadRequest("Hata");
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, CreateImageReq imageIn)
        {
            if (_imageService.Get(id) == null)
            {
                return NotFound();
            }

            var image = new Image(SaveByteArrayAsImage(imageIn.FileName, imageIn.Base64Image));
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
        private string SaveByteArrayAsImage(string fileName, string base64String)
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
