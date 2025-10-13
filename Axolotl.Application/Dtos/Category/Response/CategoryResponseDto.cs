namespace Axolotl.Application.Dtos.Category.Response;

public class CategoryResponseDto
{
    public Guid CategoryId {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime AuditCreateDate { get; set; }
    

}