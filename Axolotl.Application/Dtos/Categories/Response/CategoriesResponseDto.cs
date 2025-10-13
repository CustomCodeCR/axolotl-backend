namespace Axolotl.Application.Dtos.Categories.Response;

public class CategoriesResponseDto
{
    public Guid CategoriesId {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime AuditCreateDate { get; set; }
    

}