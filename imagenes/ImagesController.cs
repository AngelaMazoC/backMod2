using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDto model)
        {
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No se subió ningún archivo.");

            var id = Guid.NewGuid(); // NUEVO ID
            var sanitizedFileName = SanitizeFileName(Path.GetFileName(model.File.FileName));
            var fileName = $"{id}_{sanitizedFileName}";
            var path = Path.Combine(_uploadsFolder, fileName);

            Directory.CreateDirectory(_uploadsFolder); // Crea la carpeta si no existe

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/api/images/{fileName}";

            var response = new ImageUploadResponseDto
            {
                Id = id,
                FileName = fileName,
                Name = model.Name,
                Position = model.Position,
                Url = url
            };

            return Ok(response);
        }


        [HttpGet]
        public IActionResult GetAllImages()
        {
            if (!Directory.Exists(_uploadsFolder))
                return NotFound("El directorio de imágenes no existe.");

            var files = Directory.GetFiles(_uploadsFolder)
                                 .Select(filePath => new ImageInfoDto
                                 {
                                     FileName = Path.GetFileName(filePath),
                                     Url = $"{Request.Scheme}://{Request.Host}/api/images/{Path.GetFileName(filePath)}"
                                 })
                                 .OrderBy(i => i.FileName)
                                 .ToList();

            return Ok(files);
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var path = Path.Combine(_uploadsFolder, fileName);
            if (!System.IO.File.Exists(path))
                return NotFound("La imagen no fue encontrada.");

            var mime = GetMimeType(fileName);
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, mime);
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteImage(string fileName)
        {
            var path = Path.Combine(_uploadsFolder, fileName);
            if (!System.IO.File.Exists(path))
                return NotFound(new DeleteImageResponseDto
                {
                    FileName = fileName,
                    Message = "La imagen no fue encontrada."
                });

            System.IO.File.Delete(path);

            return Ok(new DeleteImageResponseDto
            {
                FileName = fileName,
                Message = "Imagen eliminada correctamente."
            });
        }

        private string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }

        private string SanitizeFileName(string input)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c, '_');
            }
            return input.Replace(" ", "_").ToLowerInvariant();
        }
    }
}