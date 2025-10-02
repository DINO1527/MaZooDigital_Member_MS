using System;

namespace MadZooDigital.Models
{
    public class Matchfee
    {
        public int MatchFeeID { get; set; }
        public int MemberID { get; set; }
        public int MatchesPlayed { get; set; }
        public decimal FeePerMatch { get; set; }
        public string MonthYear { get; set; }
        public decimal SubTotal { get; set; }
    }
}
