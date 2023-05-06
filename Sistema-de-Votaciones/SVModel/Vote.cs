namespace SVModel
{
    public class Vote
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int CandidateId { get; set; }

        public int Vote1 { get; set; }

        public DateTime Date { get; set; }

        public string IpAddress { get; set; } = null!;        
    }
}