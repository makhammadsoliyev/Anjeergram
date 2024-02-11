using Anjeergram.Models.Commons;

namespace Anjeergram.Models.Categories;

public class Category : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
}