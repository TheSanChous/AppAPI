using System.Collections.Generic;

namespace Models.Species
{
    public class Group : BaseEntity
    { 
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
