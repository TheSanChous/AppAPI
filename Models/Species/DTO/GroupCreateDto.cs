using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Species.DTO
{
    public class GroupCreateDto : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
