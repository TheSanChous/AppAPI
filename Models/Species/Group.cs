using System.Collections.Generic;

namespace Models.Species
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
