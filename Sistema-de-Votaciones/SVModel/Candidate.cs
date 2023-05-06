using System.ComponentModel.DataAnnotations;

namespace SVModel
{
    public class Candidate
    {
        public int Id { get; set; }

        [RegularExpression(@"^[1-9]\d{12}$", ErrorMessage = "Enter a validate DPI")]
        public string Dpi { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Party { get; set; } = null!;

        public string Proposal { get; set; } = null!;

        public int TotalVotes { get; set; }

        public int NullVotes { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }
    }
}