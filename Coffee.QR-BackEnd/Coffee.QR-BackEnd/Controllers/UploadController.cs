using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;


public class EmailRequest
{
    public string Destination { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

public class EmailWithAttachmentRequest : EmailRequest
{
    public IFormFile Attachment { get; set; }
}

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly IEmailSender _emailSender;

    public UploadController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

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

    [HttpPost("send-email")]
    public IActionResult SendBasicEmail([FromBody] EmailRequest emailRequest)
    {
        var result = _emailSender.SendEmail(emailRequest.Destination, emailRequest.Subject, emailRequest.Body);
        return Ok("Email sent successfully.");
    }


    [HttpPost("send-email-with-attachment")]
    public IActionResult SendEmailWithAttachment([FromForm] EmailWithAttachmentRequest request)
    {
        var result =  _emailSender.SendEmailWithAttachment(request.Destination, request.Subject, request.Body, request.Attachment.FileName);
        if (result.IsSuccess)
        {
            return Ok("Email with attachment sent successfully.");
        }
        else
        {
            return BadRequest("Failed to send email with attachment.");
        }
    }

}
