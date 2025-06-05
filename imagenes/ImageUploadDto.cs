using Microsoft.AspNetCore.Mvc;

public class ImageUploadDto
{
    [FromForm(Name = "file")]
    public IFormFile? File { get; set; }

    [FromForm(Name = "name")]
    public string? Name { get; set; }

    [FromForm(Name = "position")]
    public string? Position { get; set; }
}


public class ImageInfoDto
{
    public string? FileName { get; set; }
    public string? Url { get; set; }
}

public class ImageUploadResponseDto
{
    public Guid Id { get; set; }
    public string? FileName { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public string? Url { get; set; }
}


public class DeleteImageResponseDto
{
    public string? FileName { get; set; }
    public string? Message { get; set; }
}