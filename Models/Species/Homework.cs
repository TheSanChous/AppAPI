namespace Models.Species
{
    public class Homework
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Subject Subject { get; set; }
    }
}
