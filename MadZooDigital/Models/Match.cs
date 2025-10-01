using System;

namespace MadZooDigital.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public int MemberID { get; set; }
        public DateTime MatchDate { get; set; }
        public decimal EntryFeeLKR { get; set; }   // default 1500
        public string Notes { get; set; }
    }
}
