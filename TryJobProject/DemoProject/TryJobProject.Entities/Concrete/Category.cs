using Core.Entites.Concrete.Common;

namespace TryJobProject.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Food>? Foods { get; set; }
    }
}
