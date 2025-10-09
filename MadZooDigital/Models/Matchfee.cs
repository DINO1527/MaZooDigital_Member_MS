using System;

namespace MadZooDigital.Models
{
    public class Matchfee
    {
        public int MatchFeeID { get; set; }
        public int MemberID { get; set; }
        public int FamilyID { get; set; }
        public int MatchesPlayed { get; set; }
        public System.DateTime Date { get; set; }
        public decimal SubTotal { get; set; }
    }
}
