using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Species
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Group Group { get; set; }

        public ICollection<Homework> Homeworks { get; set; }
    }
}
