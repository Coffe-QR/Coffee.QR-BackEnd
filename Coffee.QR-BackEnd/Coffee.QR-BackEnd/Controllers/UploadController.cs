using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    /*[HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        // Ensure directory exists
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        // Save the file
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return the URL path of the uploaded file
        var fileUrl = $"/images/{fileName}";
        return Ok(new { FileUrl = fileUrl });
    }*/
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Images", file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { path = "/images/" + file.FileName });
    }
}
