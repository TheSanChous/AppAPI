using Data.Models;

namespace AppAPI.DTO
{
    public class GroupCreateModel : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
