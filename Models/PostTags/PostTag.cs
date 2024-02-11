using Anjeergram.Models.Commons;

namespace Anjeergram.Models.PostTags;

public class PostTag : Auditable
{
    public long PostId { get; set; }
    public long TagId { get; set; }
}
