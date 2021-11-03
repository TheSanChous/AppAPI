using System.Collections.Generic;

namespace Data.Models.Species
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Group Group { get; set; }

        public ICollection<Homework> HomeworkCollection { get; set; }
    }
}
