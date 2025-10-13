namespace Axolotl.Application.Dtos.Category.Response;

public class CategoryByIdResponseDto
{
    public Guid CategoryId {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}

}