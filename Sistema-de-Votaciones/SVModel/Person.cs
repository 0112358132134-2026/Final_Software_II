namespace SVModel
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Dpi { get; set; } = null!;

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}