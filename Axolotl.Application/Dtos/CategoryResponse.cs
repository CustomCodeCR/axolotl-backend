namespace Axolotl.Application.Dtos
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Navigation property to hold the count of related products
        public int ProductCount { get; set; }
    }

}