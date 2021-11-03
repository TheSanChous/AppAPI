using System.Collections.Generic;

namespace Data.Models.Species
{
    public class Group : BaseEntity
    { 
        public string Identifier { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
