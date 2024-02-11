using Anjeergram.Models.Categories;
using Anjeergram.Models.Posts;

namespace Anjeergram.Models.PostCategories;

public class PostCategoryViewModel
{
    public long Id { get; set; }
    public PostViewModel Post { get; set; }
    public CategoryViewModel Category { get; set; }
}