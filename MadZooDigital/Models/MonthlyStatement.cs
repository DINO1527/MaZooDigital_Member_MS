using System;

namespace MadZooDigital.Models
{
    public class MonthlyStatement
    {
        public int StatementID { get; set; }
        public int MemberID { get; set; }
        public DateTime StatementMonth { get; set; }
        public decimal BaseFeeLKR { get; set; }
        public decimal CoachingFeeLKR { get; set; }
        public decimal MatchFeeLKR { get; set; }
        public decimal TotalLKR { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
