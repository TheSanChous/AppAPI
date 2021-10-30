namespace Models.Species
{
    public class Homework : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Subject Subject { get; set; }
    }
}
