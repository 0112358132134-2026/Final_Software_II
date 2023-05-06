namespace SVModel
{
    public class Candidate
    {
        public int Id { get; set; }

        public string Dpi { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Party { get; set; } = null!;

        public string Proposal { get; set; } = null!;

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}