using Anjeergram.Models.Commons;

namespace Anjeergram.Models.PostCategories;

public class PostCategory : Auditable
{
    public long PostId { get; set; }
    public long CategoryId { get; set; }
}
